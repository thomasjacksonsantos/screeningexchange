using ScreeningExchange.Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.App.Api.Features.Questions.FindAllFlows;

public static class DependencyBuilder
{
    public static IServiceCollection AddFindAllFlowsFeature(this IServiceCollection services)
    {
        services.AddTransient<IUseCaseOutputPort<Result<FindAllFlowsResponse>>, JsonPresenter>();
        services.AddTransient<IInputOutputPortUseCase<FindAllFlowsRequest, IUseCaseOutputPort<Result<FindAllFlowsResponse>>, Result<FindAllFlowsResponse>>, UseCase>();
        return services;
    }
}