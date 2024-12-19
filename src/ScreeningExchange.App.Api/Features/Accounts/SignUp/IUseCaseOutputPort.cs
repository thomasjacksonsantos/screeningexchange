
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Accounts.SignUp;

public interface IUseCaseOutputPort<TResult> : IOutputPort<TResult>
{
    TResult Ok(SignUpResponse response);
    TResult BadRequest(string message);
}