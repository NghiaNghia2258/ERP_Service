namespace ERP_Service.Application.Services.Interfaces;

public interface IMailService
{
    Task SendEmailAsync(string toEmail, string subject, string bodyHtml);
}
