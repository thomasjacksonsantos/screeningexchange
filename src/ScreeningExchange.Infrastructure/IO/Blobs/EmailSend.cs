using System.Net;
using System.Net.Mail;
using ScreeningExchange.Infrastructure.Configuration;
using Microsoft.Extensions.Options;

namespace ScreeningExchange.Infrastructure.IO.Blobs;

public sealed class EmailSend : IEmailSend
{
    private readonly ApiConfig apiConfig;

    public EmailSend(ApiConfig apiConfig)
    {
        this.apiConfig = apiConfig;
    }

    public async ValueTask<(bool Success, string Message)> SendAsync(EmailParams emailParams)
    {
        try
        {
            using var smtpClient = new SmtpClient(apiConfig.Email.Smtp);
            smtpClient.Port = apiConfig.Email.Port;
            smtpClient.EnableSsl = true;
            smtpClient.Credentials = new NetworkCredential(apiConfig.Email.Login, apiConfig.Email.Password);

            var emailMessage = new MailMessage
            {
                From = new MailAddress(apiConfig.Email.From, apiConfig.Email.DisplayName),
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