namespace ERP_Service.Application.Services.VNPay;

public class VnPayRequestModel
{
    public decimal Amount { get; set; }
    public string OrderDescription { get; set; }
    public string IpAddress { get; set; }
    public string ReturnUrl { get; set; }
}
