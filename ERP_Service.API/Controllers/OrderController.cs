using ERP_Service.Application.Comands.Orders;
using ERP_Service.Application.Mapper.Model.Orders;
using ERP_Service.Application.Queries.Orders;
using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Domain.Const;
using ERP_Service.Domain.PagingRequest;
using ERP_Service.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERP_Service.API.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class OrderController : ControllerBase
	{
		private readonly IAuthoziService _authoziService;
		private readonly IMediator _mediator;
		private readonly AppDbContext _dbContext;


        public OrderController(IAuthoziService authoziService, IMediator mediator, AppDbContext dbContext)
        {
            _authoziService = authoziService;
            _mediator = mediator;
            _dbContext = dbContext;
        }
        [HttpGet("{id:guid}")]
		public async Task<IActionResult> GetById(Guid id)
		{
			var order = await _dbContext.Orders
				.Include(x => x.ShippingAddress)
				.Include(x => x.OrderItems)
					.ThenInclude(x => x.ProductVariant)
					.ThenInclude(x => x.Product)
					.Where(x => x.Id == id)
					.Select(x => new OrderDto
					{
						Code = x.Code,
						CreatedAt = x.CreatedAt,
						CreatedBy = x.CreatedBy,
						CreatedName = x.CreatedName,
						CustomerName = x.CustomerName,
						CustomerNote = x.CustomerNote,
						CustomerPhone = x.CustomerPhone,
						DiscountPercent = x.DiscountPercent ?? 0,
						DiscountValue = x.DiscountValue ?? 0,
						Id = id,
						PaymentStatus = x.PaymentStatus,
						Name = x.Name,
						Note = x.Note,
						TotalPrice = x.OrderItems.Sum(y => y.UnitPrice * y.Quantity),
						StoreName = x.StoreName,
						VoucherCode = x.VoucherCode,
						ShippingAddress = new ShippingAddressDto
						{
							FullAddress = $"{x.ShippingAddress.Ward}, {x.ShippingAddress.Province}",
							PhoneNumber = x.ShippingAddress.PhoneNumber,
							RecipientName = x.ShippingAddress.FullName,
						},
						OrderItems = x.OrderItems.Select(y => new OrderItemDto
						{
							Id = y.Id,
							ImageUrl = y.ImageUrl,	
							Quantity = y.Quantity,
							UnitPrice = y.UnitPrice,
							ProductVariant = new ProductVariantOrderDto
							{
								Name = y.ProductVariant.Product.Name,
								PropertyValue1 = y.ProductVariant.PropertyValue1,
								PropertyValue2 = y.ProductVariant.PropertyValue2,
                            }
						}).ToList(),
					}).FirstOrDefaultAsync();

			return Ok(order);
		}
		[HttpPost]
		public async Task<IActionResult> Create()
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.CREATE_CUSTOMER);

			var result = await _mediator.Send(new CreateOrderUsePOSCommand());
			return Ok(result);
		}
		[HttpPut]
		public async Task<IActionResult> Update(UpdateOrderDto orderDto)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.UPDATE_CUSTOMER);

			var result = await _mediator.Send(new UpdateOrderCommand(orderDto));
			return Ok(result);
		}
		[HttpDelete("{id:guid}")]
		public async Task<IActionResult> Delete(Guid id)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.DELETE_CUSTOMER);

			var result = await _mediator.Send(new DeleteOrderCommand(id));
			return Ok(result);
		}
		[HttpGet("payment/{orderId}")]
		public async Task<IActionResult> Payment(Guid orderId)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.SELECT_CUSTOMER);

			var result = await _mediator.Send(new PaymentCommand(orderId));
			return Ok(result);
		}
		[HttpDelete("remove-from-cart/{orderItemId}")]
		public async Task<IActionResult> RemoveFromCart(int orderItemId)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.SELECT_CUSTOMER);

			var result = await _mediator.Send(new RemoveFromCartCommand(orderItemId));
			return Ok(result);
		}
		[HttpGet("get-order-not-completed")]
		public async Task<IActionResult> GetOrderNotCompleted([FromQuery] OptionFilterOrder option)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.SELECT_CUSTOMER);

			var result = await _mediator.Send(new GetOrderNotCompletedQuery(option));
			return Ok(result);
		}
		[HttpGet("get-all")]
		public async Task<IActionResult> GetAll([FromQuery] OptionFilterOrder option)
		{
			await _authoziService.IsAuthozi(role: RoleNameConst.SELECT_CUSTOMER);

			var result = await _mediator.Send(new GetAllOrderQuery(option));
			return Ok(result);
		}
	}
}
