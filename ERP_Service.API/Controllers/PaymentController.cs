using ERP_Service.Application.Services.VNPay;
using Microsoft.AspNetCore.Mvc;
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

    public PaymentController(IVnpay vnPayservice, IConfiguration configuration)
    {
        _vnpay = vnPayservice;
        _configuration = configuration;

        _vnpay.Initialize(_configuration["Vnpay:TmnCode"], _configuration["Vnpay:HashSecret"], _configuration["Vnpay:BaseUrl"], _configuration["Vnpay:CallbackUrl"]);
    }
    [HttpGet("CreatePaymentUrl")]
    public ActionResult<string> CreatePaymentUrl(double money, string description)
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
                BankCode = BankCode.ANY, // Tùy chọn. Mặc định là tất cả phương thức giao dịch
                CreatedDate = DateTime.Now, // Tùy chọn. Mặc định là thời điểm hiện tại
                Currency = Currency.VND, // Tùy chọn. Mặc định là VND (Việt Nam đồng)
                Language = DisplayLanguage.Vietnamese // Tùy chọn. Mặc định là tiếng Việt
            };

            var paymentUrl = _vnpay.GetPaymentUrl(request);

            return Created(paymentUrl, paymentUrl);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
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
