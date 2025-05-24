using ERP_Service.Application.Services.VNPay;
using Microsoft.AspNetCore.Mvc;

namespace ERP_Service.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PaymentController(
    IVnPayService _vnPayService
) : ControllerBase
{

    [HttpPost("create")]
    public IActionResult CreatePayment([FromBody] VnPayRequestModel model)
    {
        var url = _vnPayService.CreatePaymentUrl(model);
        return Ok(new { paymentUrl = url });
    }

    [HttpGet("callback")]
    public IActionResult Callback()
    {
        bool isValid = _vnPayService.ValidateResponse(Request.Query);
        return Ok(new { isValid });
    }

}
