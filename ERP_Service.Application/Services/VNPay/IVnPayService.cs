using Microsoft.AspNetCore.Http;

namespace ERP_Service.Application.Services.VNPay;

public interface IVnPayService
{
    string CreatePaymentUrl(VnPayRequestModel model);
    bool ValidateResponse(IQueryCollection query);
}
