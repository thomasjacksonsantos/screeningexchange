using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Accounts.SignIn;

public class JsonPresenter : IUseCaseOutputPort<Result<SignInResponse>>
{
    public Result<SignInResponse> BadRequest(string message)
    {
        return Result.Fail<SignInResponse>(
            message
        );
    }

    public Result<SignInResponse> Ok(SignInResponse response)
    {
        return Result.Ok<SignInResponse>(
            response
        );
    }
}