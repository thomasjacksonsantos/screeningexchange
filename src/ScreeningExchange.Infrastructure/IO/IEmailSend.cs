namespace ScreeningExchange.Infrastructure.IO;

public record EmailParams(
    string To,
    string Subject,
    string Message
);

public interface IEmailSend
{
    public ValueTask<(bool Success, string Message)> SendAsync(
        EmailParams emailParams
    );
}