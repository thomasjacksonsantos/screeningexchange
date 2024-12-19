namespace ScreeningExchange.App.Api.Features.Accounts.SignIn;

public record class SignInResponse
(
    string IdToken,
    string? Message = null
);