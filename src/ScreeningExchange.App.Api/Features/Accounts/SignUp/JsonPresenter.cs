using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Accounts.SignUp;

public class JsonPresenter : IUseCaseOutputPort<Result<SignUpResponse>>
{
    public Result<SignUpResponse> BadRequest(string message)
    {
        return Result.Fail<SignUpResponse>(
            message
        );
    }

    public Result<SignUpResponse> Ok(SignUpResponse response)
    {
        return Result.Ok<SignUpResponse>(
            response
        );
    }
}