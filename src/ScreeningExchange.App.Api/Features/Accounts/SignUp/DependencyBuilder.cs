using ScreeningExchange.Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.App.Api.Features.Accounts.SignUp;

public static class DependencyBuilder
{
    public static IServiceCollection AddSignUpFeature(this IServiceCollection services)
    {
        services.AddTransient<IUseCaseOutputPort<Result<SignUpResponse>>, JsonPresenter>();
        services.AddTransient<IInputOutputPortUseCase<SignUpRequest, IUseCaseOutputPort<Result<SignUpResponse>>, Result<SignUpResponse>>, UseCase>();
        return services;
    }
}