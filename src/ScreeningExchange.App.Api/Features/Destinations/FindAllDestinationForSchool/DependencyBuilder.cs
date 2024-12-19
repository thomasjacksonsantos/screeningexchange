using ScreeningExchange.Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.App.Api.Features.Destinations.FindAllDestinationForSchool;

public static class DependencyBuilder
{
    public static IServiceCollection AddFindAllDestinationForSchoolFeature(this IServiceCollection services)
    {
        services.AddTransient<IUseCaseOutputPort<Result<FindAllDestinationForSchoolResponse>>, JsonPresenter>();
        services.AddTransient<IInputOutputPortUseCase<FindAllDestinationForSchoolIdRequest, IUseCaseOutputPort<Result<FindAllDestinationForSchoolResponse>>, Result<FindAllDestinationForSchoolResponse>>, UseCase>();
        return services;
    }
}