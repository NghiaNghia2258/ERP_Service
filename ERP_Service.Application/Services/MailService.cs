using ERP_Service.Application.Services.Interfaces;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Configuration;
using MimeKit;

namespace ERP_Service.Application.Services;

public class MailService : IMailService
{
    private readonly IConfiguration _configuration;

    public MailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string bodyHtml)
    {
        var smtpHost = _configuration["Mail:SmtpHost"];
        var smtpPort = int.Parse(_configuration["Mail:SmtpPort"] ?? "587");
        var smtpUser = _configuration["Mail:SmtpUser"];
        var smtpPass = _configuration["Mail:SmtpPass"];
        var fromEmail = _configuration["Mail:FromEmail"];
        var fromName = _configuration["Mail:FromName"];

        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(fromName, fromEmail));
        message.To.Add(MailboxAddress.Parse(toEmail));
        message.Subject = subject;

        var bodyBuilder = new BodyBuilder
        {
            HtmlBody = bodyHtml
        };

        message.Body = bodyBuilder.ToMessageBody();

        using var smtp = new SmtpClient();

        smtp.ServerCertificateValidationCallback = (s, c, h, e) => true;

        try
        {
            await smtp.ConnectAsync(smtpHost, smtpPort, SecureSocketOptions.StartTls);
            await smtp.AuthenticateAsync(smtpUser, smtpPass);
            await smtp.SendAsync(message);
            await smtp.DisconnectAsync(true);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Gửi email thất bại: {ex.Message}", ex);
        }
    }
}
