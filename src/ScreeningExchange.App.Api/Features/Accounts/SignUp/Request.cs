
namespace ScreeningExchange.App.Api.Features.Accounts.SignUp;

public record SignUpRequest(
    string Displayname,
    string Email,
    string Password
) ;