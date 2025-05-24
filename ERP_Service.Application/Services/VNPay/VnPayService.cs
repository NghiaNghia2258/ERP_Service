using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;

namespace ERP_Service.Application.Services.VNPay;

public class VnPayService : IVnPayService
{
    private readonly IConfiguration _config;

    public VnPayService(IConfiguration config)
    {
        _config = config;
    }

    public string CreatePaymentUrl(VnPayRequestModel model)
    {
        var vnpay = new VnPayLibrary();
        var tick = DateTime.Now.Ticks.ToString();

        vnpay.AddRequestData("vnp_Version", "2.1.0");
        vnpay.AddRequestData("vnp_Command", "pay");
        vnpay.AddRequestData("vnp_TmnCode", _config["VNPay:TmnCode"]);
        vnpay.AddRequestData("vnp_Amount", ((int)(model.Amount * 100)).ToString());
        vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
        vnpay.AddRequestData("vnp_CurrCode", "VND");
        vnpay.AddRequestData("vnp_IpAddr", model.IpAddress);
        vnpay.AddRequestData("vnp_Locale", "vn");
        vnpay.AddRequestData("vnp_OrderInfo", model.OrderDescription);
        vnpay.AddRequestData("vnp_OrderType", "other");
        vnpay.AddRequestData("vnp_ReturnUrl", model.ReturnUrl);
        vnpay.AddRequestData("vnp_TxnRef", tick);

        return vnpay.CreateRequestUrl(_config["VNPay:BaseUrl"], _config["VNPay:HashSecret"]);
    }

    public bool ValidateResponse(IQueryCollection query)
    {
        var vnpay = new VnPayLibrary();
        foreach (var (key, value) in query)
        {
            if (!string.IsNullOrEmpty(key) && key.StartsWith("vnp_"))
            {
                vnpay.AddResponseData(key, value);
            }
        }

        string vnp_SecureHash = query["vnp_SecureHash"];
        return vnpay.ValidateSignature(vnp_SecureHash, _config["VNPay:HashSecret"]);
    }
}
