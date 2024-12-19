namespace ScreeningExchange.App.Api.Features.Accounts.SignUp;

public record class SignUpResponse
(
    string IdToken,
    string? Message = null
);