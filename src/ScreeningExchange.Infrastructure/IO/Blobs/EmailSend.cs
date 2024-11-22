using System.Net;
using System.Net.Mail;
using ScreeningExchange.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace ScreeningExchange.Infrastructure.IO.Blobs;

public sealed class EmailSend(IOptions<ApiConfig> config) : IEmailSend
{
    private readonly ApiConfig Config = config.Value;
    public async ValueTask<(bool Success, string Message)> SendAsync(EmailParams emailParams)
    {
        try
        {
            using var smtpClient = new SmtpClient(Config.Email.Smtp);
            smtpClient.Port = Config.Email.Port;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(Config.Email.Login, Config.Email.Password);

            var emailMessage = new MailMessage
            {
                From = new MailAddress(Config.Email.From, Config.Email.DisplayName),
                Subject = emailParams.Subject,
                Body = emailParams.Message,
                IsBodyHtml = true
            };

            emailMessage.To.Add(
                new MailAddress(
                    emailParams.To
                )
            );

            await smtpClient.SendMailAsync(emailMessage);
        }
        catch (System.Exception ex)
        {

            return (false, ex.Message);
        }

        return (true, "Send email with success.");
    }
}