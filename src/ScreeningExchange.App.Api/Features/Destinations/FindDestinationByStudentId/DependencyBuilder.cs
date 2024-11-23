using ScreeningExchange.Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.App.Api.Features.Destinations.FindDestinationByStudentId;

public static class DependencyBuilder
{
    public static IServiceCollection AddFindDestinationByStudentIdFeature(this IServiceCollection services)
    {
        services.AddTransient<IUseCaseOutputPort<Result<FindDestinationByStudentIdResponse>>, JsonPresenter>();
        services.AddTransient<IInputOutputPortUseCase<FindDestinationByStudentIddRequest, IUseCaseOutputPort<Result<FindDestinationByStudentIdResponse>>, Result<FindDestinationByStudentIdResponse>>, UseCase>();
        return services;
    }
}