
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Accounts.SignIn;

public interface IUseCaseOutputPort<TResult> : IOutputPort<TResult>
{
    TResult Ok(SignInResponse response);
    TResult BadRequest(string message);
}