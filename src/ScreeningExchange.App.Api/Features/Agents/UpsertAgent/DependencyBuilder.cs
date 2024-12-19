using ScreeningExchange.Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.App.Api.Features.Agents.UpsertAgent;

public static class DependencyBuilder
{
    public static IServiceCollection AddStudentUpsertAgentFeature(this IServiceCollection services)
    {
        services.AddTransient<IUseCaseOutputPort<Result<UpsertAgentResponse>>, JsonPresenter>();
        services.AddTransient<IInputOutputPortUseCase<UpsertAgentRequest, IUseCaseOutputPort<Result<UpsertAgentResponse>>, Result<UpsertAgentResponse>>, UseCase>();
        return services;
    }
}