namespace ScreeningExchange.Infrastructure.IO;

public record WhatsappParams(
    long ToNumber,
    string Message
);

public interface IWhatsapp
{
    public ValueTask<(string ErrorMessage, string ErrorCode)> SendAsync(
        WhatsappParams whatsappParams
    );
}