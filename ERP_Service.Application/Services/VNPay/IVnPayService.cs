using Microsoft.AspNetCore.Http;

namespace ERP_Service.Application.Services.VNPay;

public interface IVnPayService
{
    string CreatePaymentUrl(PaymentInformationModel model, HttpContext context);
    PaymentResponseModel PaymentExecute(IQueryCollection collections);
}
