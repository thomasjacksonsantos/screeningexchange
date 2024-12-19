using ScreeningExchange.Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.App.Api.Features.Accounts.SignIn;

public static class DependencyBuilder
{
    public static IServiceCollection AddSignInFeature(this IServiceCollection services)
    {
        services.AddTransient<IUseCaseOutputPort<Result<SignInResponse>>, JsonPresenter>();
        services.AddTransient<IInputOutputPortUseCase<SignInRequest, IUseCaseOutputPort<Result<SignInResponse>>, Result<SignInResponse>>, UseCase>();
        return services;
    }
}