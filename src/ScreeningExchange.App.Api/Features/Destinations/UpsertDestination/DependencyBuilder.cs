using ScreeningExchange.Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.App.Api.Features.Destinations.UpsertDestination;

public static class DependencyBuilder
{
    public static IServiceCollection AddUpsertDestinationFeature(this IServiceCollection services)
    {
        services.AddTransient<IUseCaseOutputPort<Result<UpsertDestinationResponse>>, JsonPresenter>();
        services.AddTransient<IInputOutputPortUseCase<UpsertDestinationdRequest, IUseCaseOutputPort<Result<UpsertDestinationResponse>>, Result<UpsertDestinationResponse>>, UseCase>();
        return services;
    }
}