using ScreeningExchange.Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.App.Api.Features.Agents.FindAgentById;

public static class DependencyBuilder
{
    public static IServiceCollection AddStudentFindAgentByIdFeature(this IServiceCollection services)
    {
        services.AddTransient<IUseCaseOutputPort<Result<FindAgentByIdResponse>>, JsonPresenter>();
        services.AddTransient<IInputOutputPortUseCase<FindAgentByIdRequest, IUseCaseOutputPort<Result<FindAgentByIdResponse>>, Result<FindAgentByIdResponse>>, UseCase>();
        return services;
    }
}