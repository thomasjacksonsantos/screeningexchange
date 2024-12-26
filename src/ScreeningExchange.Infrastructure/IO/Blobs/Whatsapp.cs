

using Microsoft.Extensions.Options;
using Twilio.Rest.Api.V2010.Account;
using Twilio;
using ScreeningExchange.Infrastructure.Configuration;

namespace ScreeningExchange.Infrastructure.IO.Blobs;

public class WhatsAppRepository : IWhatsapp
{
    private readonly ApiConfig apiConfig;
    public WhatsAppRepository(IOptions<ApiConfig> apiConfig)
        => (this.apiConfig) = (apiConfig.Value);

    CreateMessageOptions CreateMessageOptions(long number)
        => new CreateMessageOptions(new Twilio.Types.PhoneNumber(FormatNumberTo(number)));

    string FormatNumberFrom(string number)
        => $"whatsapp:+{number}";

    string FormatNumberTo(long number)
        => $"whatsapp:+55{number}";

    public async ValueTask<(string ErrorMessage, string ErrorCode)> SendAsync(WhatsappParams whatsappParams)
    {
        TwilioClient.Init(apiConfig.WhatsApp.AccountSid, apiConfig.WhatsApp.AuthToken);
        var messageOptions = CreateMessageOptions(whatsappParams.ToNumber);
        messageOptions.From = FormatNumberFrom(apiConfig.WhatsApp.NumberPhone);
        messageOptions.Body = whatsappParams.Message;
        var response = await MessageResource.CreateAsync(messageOptions);

        return (
            response.ErrorMessage,
            response.ErrorCode?.ToString() ?? string.Empty
        );
    }
}