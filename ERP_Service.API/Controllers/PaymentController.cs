using ERP_Service.Application.Services.Interfaces;
using ERP_Service.Application.Services.VNPay;
using ERP_Service.Domain.Const;
using ERP_Service.Domain.Models.Orders;
using ERP_Service.Infrastructure;
using ERP_Service.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using System.Linq;
using VNPAY.NET;
using VNPAY.NET.Enums;
using VNPAY.NET.Models;
using VNPAY.NET.Utilities;

namespace ERP_Service.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController: ControllerBase
{
    private readonly IVnpay _vnpay;
    private readonly IConfiguration _configuration;
    private readonly AppDbContext _dbContext;
    private readonly IAuthoziService _authoziService;

    public PaymentController(IVnpay vnPayservice, IConfiguration configuration, AppDbContext _dbContext, IAuthoziService _authoziService)
    {
        _vnpay = vnPayservice;
        _configuration = configuration;
        this._dbContext = _dbContext;
        this._authoziService = _authoziService;

        _vnpay.Initialize(_configuration["Vnpay:TmnCode"], _configuration["Vnpay:HashSecret"], _configuration["Vnpay:BaseUrl"], _configuration["Vnpay:CallbackUrl"]);
    }
    [HttpGet("CreatePaymentUrl")]
    public ActionResult<string> CreatePaymentUrl(double money, string description, string shippingId)
    {
        try
        {
            var ipAddress = NetworkHelper.GetIpAddress(HttpContext); 

            var request = new PaymentRequest
            {
                PaymentId = DateTime.Now.Ticks,
                Money = money,
                Description = description,
                IpAddress = ipAddress,
                BankCode = BankCode.ANY, 
                CreatedDate = DateTime.Now,
                Currency = Currency.VND, 
                Language = DisplayLanguage.Vietnamese 
            };
            PayloadToken token = _authoziService.PayloadToken;
            var cart = _dbContext.Carts.FirstOrDefault(x => !x.HasOrder && x.CustomerId == token.CustomerId);
            cart.ShipingAddressId = shippingId;
            cart.PaymentId = request.PaymentId;
            _dbContext.SaveChanges();
            var paymentUrl = _vnpay.GetPaymentUrl(request);

            return Created(paymentUrl, paymentUrl);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpGet("cod")]
    public async Task<IActionResult> COD(string shippingId)
    {
        PayloadToken token = _authoziService.PayloadToken;
        var cart = await _dbContext.Carts
            .Where(x => !x.HasOrder && x.CustomerId == token.CustomerId).FirstOrDefaultAsync();
        var cartItems = _dbContext.CartItem.Where(x => x.CartId == cart.Id && x.StoreId != null).GroupBy(x => x.StoreId).ToDictionary(g => g.Key, g => g.ToList());
        var shipping = _dbContext.ShippingAddresses.FirstOrDefault(x => x.Id == shippingId);
        foreach (var cartItem in cartItems)
        {
            Guid? key = cartItem.Key;
            List<CartItem> values = cartItem.Value;
            var storeName = _dbContext.Stores.Where(x => x.Id == key).Select(x => x.Name).FirstOrDefault();

            var newOrder = new Order()
            {
                StoreName = storeName,
                StoreId = key ?? new Guid(),
                Code = key.ToString(),
                CustomerName = shipping.FullName,
                CustomerPhone = shipping.PhoneNumber,
                ShippingAddressId = cart.ShipingAddressId,
                CreatedAt = DateTime.Now,
                CreatedBy = "COD",
                CreatedName = "COD",
                CustomerId = cart.CustomerId,
                PaymentStatus = StatusOrder.Pending,
                OrderItems = new List<OrderItem>()
            };
            foreach (var item in values)
            {
                newOrder.OrderItems.Add(new OrderItem
                {
                    ImageUrl = item.ImageUrl,
                    ProductVariantId = item.ProductVariantId,
                    Quantity = item.Quantity,
                    UnitPrice = item.UnitPrice,
                    Version = item.Version,
                });
            }
            _dbContext.Orders.Add(newOrder);
        }
        cart.HasOrder = true;
        _dbContext.SaveChanges();
        return Ok(true);
    }

    [HttpGet("Callback")]
    public ActionResult<PaymentResult> Callback()
    {
        if (Request.QueryString.HasValue)
        {
            try
            {
                var paymentResult = _vnpay.GetPaymentResult(Request.Query);

                if (paymentResult.IsSuccess)
                {
                    var cart = _dbContext.Carts.FirstOrDefault(x => x.PaymentId == paymentResult.PaymentId);
                    var cartItems = _dbContext.CartItem.Where(x => x.CartId == cart.Id && x.StoreId != null).GroupBy(x => x.StoreId).ToDictionary(g => g.Key, g => g.ToList());
                    var shipping = _dbContext.ShippingAddresses.FirstOrDefault(x => x.Id == cart.ShipingAddressId);

                    foreach (var cartItem in cartItems)
                    {
                        Guid? key = cartItem.Key;
                        List<CartItem> values = cartItem.Value;

                        var newOrder = new Order()
                        {
                            StoreId = key ?? new Guid(),
                            Code = key.ToString(),
                            CustomerName = shipping.FullName,
                            CustomerPhone = shipping.PhoneNumber,
                            ShippingAddressId = cart.ShipingAddressId,
                            CreatedAt = DateTime.Now,
                            CreatedBy = "VNPay",
                            CreatedName = "VNPay",
                            CustomerId = cart.CustomerId,
                            PaymentStatus = StatusOrder.Pending,
                            OrderItems = new List<OrderItem>()
                        };
                        foreach (var item in values)
                        {
                            newOrder.OrderItems.Add(new OrderItem
                            {
                                ImageUrl = item.ImageUrl,
                                ProductVariantId = item.ProductVariantId,
                                Quantity = item.Quantity, 
                                UnitPrice = item.UnitPrice,
                                Version = item.Version,
                            });    
                        }
                       _dbContext.Orders.Add(newOrder);
                    }
                    cart.HasOrder = true;
                    _dbContext.SaveChanges();
                    return Ok(paymentResult);
                }

                return BadRequest(paymentResult);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        return NotFound("Không tìm thấy thông tin thanh toán.");
    }
}
