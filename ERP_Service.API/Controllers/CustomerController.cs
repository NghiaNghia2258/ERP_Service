using ERP_Service.Application.Comands.Customers;
using ERP_Service.Application.Mapper.Model.Carts;
using ERP_Service.Application.Mapper.Model.Customers;
using ERP_Service.Application.Queries.Customers;
using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.ApiResult;
using ERP_Service.Domain.Const;
using ERP_Service.Domain.Models;
using ERP_Service.Domain.Models.Orders;
using ERP_Service.Domain.PagingRequest;
using ERP_Service.Infrastructure;
using ERP_Service.Shared.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace ERP_Service.API.Controllers
{
    [Route("api/[controller]")]
	[ApiController]
	public class CustomerController(IEventBufferService _eventBufferService, IAuthoziService _authoziService, IMediator _mediator, AppDbContext _dbContext) : ControllerBase
	{
        [HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.SELECT_CUSTOMER);

			var result = await _mediator.Send(new GetByIdCustomerCommand(id));
			return Ok(result);
		}

		[HttpGet]
		public async Task<IActionResult> GetAll([FromQuery] OptionFilterCustomer option)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.SELECT_CUSTOMER);

            PayloadToken token = _authoziService.PayloadToken;

			int begin = (option.PageIndex - 1) * option.PageSize;
			int take = option.PageSize;


            var result = await _dbContext.Customers.Where(x => string.IsNullOrEmpty(option.KeyWord) || x.Name.Contains(option.KeyWord ?? "")
			)
				.Select(x => new
				{
					Id = x.Id,
					Name = x.Name,
					Phone = x.Phone,
					Email = x.Email,
					IsActive = x.IsActive

				}).Skip(begin).Take(take).ToListAsync();
			return Ok(result);
		}
        [HttpGet("getCustomerStats")]
        public async Task<IActionResult> GetCustomerStats()
        {
            PayloadToken token = _authoziService.PayloadToken;

            Guid id = token.CustomerId;
            var customer = await _dbContext.Customers.FindAsync(id);
            if (customer == null) return NotFound();

            var orders = _dbContext.Orders
                .Where(o => o.CustomerId == id);

            var totalOrders = await orders.CountAsync();
            var totalSpent = await orders.SumAsync(o => (double?)o.TotalPrice) ?? 0;
            var canceledOrders = await orders.CountAsync(o => o.PaymentStatus == 0); 
            var lastOrderDate = await orders
                .OrderByDescending(o => o.CreatedAt)
                .Select(o => (DateTime?)o.CreatedAt)
                .FirstOrDefaultAsync();

            // Tính sản phẩm mua nhiều nhất
            var topProduct = await _dbContext.OrderItems
             .Where(d => d.Order.CustomerId == id)
             .GroupBy(d => d.ProductVariantId)
             .Select(g => new {
                 ProductVariantId = g.Key,
                 TimesPurchased = g.Sum(x => x.Quantity)
             })
             .OrderByDescending(g => g.TimesPurchased)
             .Join(_dbContext.ProductVariants,
                   g => g.ProductVariantId,
                   v => v.Id,
                   (g, v) => new {
                       v.ProductId,
                       g.TimesPurchased
                   })
             .Join(_dbContext.Products,
                   gv => gv.ProductId,
                   p => p.Id,
                   (gv, p) => new {
                       Name = p.Name,
                       TimesPurchased = gv.TimesPurchased
                   })
             .FirstOrDefaultAsync();

            var totalReviews = await _dbContext.ProductRates
                .CountAsync(r => r.CustomerId == id && !string.IsNullOrEmpty(r.Review));

            return Ok(new
            {
                totalOrders,
                totalSpent,
                cancelRate = totalOrders == 0 ? 0 : Math.Round((double)canceledOrders / totalOrders, 4),
                lastOrderDate,
                topProduct = topProduct ?? new { Name = "", TimesPurchased = 0 },
                joinedAt = customer.CreatedAt,
                isVerified = customer.IsActive ?? false,
                totalReviews,
            });
        }
        [HttpGet("addOrRemoveWishList")]
        public async Task<IActionResult> AddOrRemoveWishList(int productId, bool isAdd)
        {
            PayloadToken token = _authoziService.PayloadToken;

            var productWish = await _dbContext.ProductWishs.FirstOrDefaultAsync(x => x.ProductId == productId);
            if(productWish is null && isAdd)
            {
                _dbContext.ProductWishs.Add(new Domain.Models.Products.ProductWish
                {
                    ProductId = productId,   
                    CustomerId = token.CustomerId
                });
            }
            else if (productWish is not null && !isAdd)
            {
                _dbContext.ProductWishs.Remove(productWish);
            }
            else
            {
                return Ok(new ApiErrorResult());
            }

            var userEvent = new UserEvent
            {
                Id = Guid.NewGuid(),
                UserId = token.CustomerId,
                ProductId = productId,
                EventTime = DateTime.UtcNow,
                Weight = isAdd ? EventWeights.AddToWishlist.Weight : EventWeights.RemoveFromWishlist.Weight,
                EventName = isAdd ? EventWeights.AddToWishlist.Name : EventWeights.RemoveFromWishlist.Name,
            };
            var eventJson = JsonSerializer.Serialize(userEvent);

            await _eventBufferService.AppendEventAsync(eventJson);

            await _dbContext.SaveChangesAsync();
            return Ok(new ApiSuccessResult<bool>(true));
        }

        [HttpGet("getCustomerStatsById/{id}")]
        public async Task<IActionResult> getCustomerStatsById(Guid id)
        {
            PayloadToken token = _authoziService.PayloadToken;

			var query = _dbContext.Customers.Where(x => x.StoreId.Equals(token.StoreId));

            var result = new
			{
				TotalCustomers = await query.CountAsync(),
				ActiveCustomers = await query.CountAsync(x => x.IsActive ?? false),
				InActiveCustomers = await query.CountAsync(x => !x.IsActive ?? true),

            };
            return Ok(result);
        }
		

        [HttpPost]
		public async Task<IActionResult> Create([FromBody] Customer model)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.CREATE_CUSTOMER);

            _dbContext.Customers.Add(model);
            await _dbContext.SaveChangesAsync();
			return Ok("");
		}
        [HttpPost("review")]
        public async Task<IActionResult> Review([FromBody] ReviewDto model)
        {
			PayloadToken token = _authoziService.PayloadToken;

            var review = await _dbContext.ProductRates.FirstOrDefaultAsync(x => x.ProductId.Equals(model.ProductId) && x.CustomerId.Equals(token.CustomerId));
			if(review is null)
			{
                await _dbContext.ProductRates.AddAsync(new Domain.Models.Products.ProductRate
                {
                    ProductId = model.ProductId,
                    Rating = model.Rating,
                    Review = model.Comment,
                    CustomerId = token.CustomerId,

                });
			}
			else
			{
				review.Rating = model.Rating;
				review.Review = model.Comment;
			}
			
			await _dbContext.SaveChangesAsync();
            return Ok("Success");
        }

        [HttpPut("{id:guid}")]
		public async Task<IActionResult> Update(Guid id, [FromBody] CustomerDto model)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.UPDATE_CUSTOMER);

			var result = await _mediator.Send(new UpdateCustomerCommand(model));
			return Ok(result);
		}

		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.DELETE_CUSTOMER);

			var result = await _mediator.Send(new DeleteCustomerCommand(id));
			return Ok(result);
		}

        [HttpPost("add-to-cart")]
        public async Task<IActionResult> AddToCart(AddToCartDto dto)
        {
            PayloadToken token = _authoziService.PayloadToken;

            //Xử lý logic thêm vào giỏ hàng
            var cart = await _dbContext.Carts
                .Include(x => x.CartItems)
                .FirstOrDefaultAsync(x => !x.HasOrder && x.CustomerId == token.CustomerId);
            if(cart is null)
            {
                cart = new Domain.Models.Orders.Cart()
                {
                    CustomerId = token.CustomerId,
                };
                await _dbContext.Carts.AddAsync(cart);
                await _dbContext.SaveChangesAsync();
            }
            var variant = await _dbContext.ProductVariants.Include(x => x.Product).FirstOrDefaultAsync(x => x.ProductId == dto.ProductId
            && x.PropertyValue1 == dto.Property1
            && x.PropertyValue2 == dto.Property2
            );
            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductVariantId == variant.Id);
            if(cartItem is null)
            {
                cartItem = new CartItem
                {
                    CartId = cart.Id,
                    ProductVariantId = variant.Id,
                    Quantity = dto.Quantity,
                    ImageUrl = variant?.ImageUrl,
                    UnitPrice = variant.Price,
                    StoreId = variant.Product.StoreId
                };
                await _dbContext.CartItem.AddAsync(cartItem);
            }
            else
            {
                cartItem.Quantity += dto.Quantity;
            }
           
            await _dbContext.SaveChangesAsync();

            var userEvent = new UserEvent
            {
                Id = Guid.NewGuid(),
                UserId = token.CustomerId,
                ProductId = dto.ProductId,
                EventTime = DateTime.UtcNow,
                Weight = EventWeights.AddToCart.Weight,
                EventName = EventWeights.AddToCart.Name
            };
            var eventJson = JsonSerializer.Serialize(userEvent);

            await _eventBufferService.AppendEventAsync(eventJson);
            return Ok(new ApiSuccessResult<bool>(true));
        }
        [HttpPost("remove-from-cart")]
        public async Task<IActionResult> RemoveFromCart(int productVariantId)
        {
            PayloadToken token = _authoziService.PayloadToken;
            var cart = await _dbContext.Carts.Include(x => x.CartItems).FirstOrDefaultAsync(x => !x.HasOrder && x.CustomerId == token.CustomerId);
            if (cart is null) { throw new Exception("Không có cart hợp lệ"); }
            var cartItem = cart.CartItems.FirstOrDefault(x => x.ProductVariantId == productVariantId);
            if (cartItem is null) { throw new Exception("Không có cartItem hợp lệ"); }
            _dbContext.CartItem.Remove(cartItem);
            await _dbContext.SaveChangesAsync();

            var productId = (await _dbContext.ProductVariants.FirstOrDefaultAsync(x => x.Id == productVariantId))?.ProductId;

            var userEvent = new UserEvent
            {
                Id = Guid.NewGuid(),
                UserId = token.CustomerId,
                ProductId = productId ?? -1,
                EventTime = DateTime.UtcNow,
                Weight = EventWeights.RemoveFromCart.Weight,
                EventName = EventWeights.RemoveFromCart.Name
            };
            var eventJson = JsonSerializer.Serialize(userEvent);

            await _eventBufferService.AppendEventAsync(eventJson);
            return Ok(new ApiSuccessResult<bool>(true));
        }
        [HttpGet("get-cart")]
        public async Task<IActionResult> GetCart()
        {
            PayloadToken token = _authoziService.PayloadToken;
            var cart = await _dbContext.Carts
                .Include(x => x.CartItems)
                    .ThenInclude(x => x.ProductVariant)
                    .ThenInclude(x => x.Product)
                    .ThenInclude(x => x.Store)
                .Where(x => !x.HasOrder && x.CustomerId == token.CustomerId)
                .Select(x => new CartDto
                {
                    Id = x.Id,
                    CartItems = x.CartItems.Select(y => new CartItemDto
                    {
                        Id = y.Id,
                        Name = $"{y.ProductVariant.Product.Name}-{y.ProductVariant.PropertyValue1}-{y.ProductVariant.PropertyValue2}",
                        ImageUrl = y.ProductVariant.ImageUrl,
                        IsSelected = y.HasSelected,
                        Price = y.UnitPrice,
                        ProductId = y.ProductVariant.Id,
                        Quantity = y.Quantity,
                        ShopAvatarUrl = y.ProductVariant.Product.Store.Logo,
                        ShopId = y.ProductVariant.Product.Store.Id,
                        ShopName = y.ProductVariant.Product.Store.Name
                    }).ToList()
                })
                .FirstOrDefaultAsync();
            if (cart is null) { throw new Exception("Không có cart hợp lệ"); }
            return Ok(new ApiSuccessResult<CartDto>(cart));
        }
    }
}
