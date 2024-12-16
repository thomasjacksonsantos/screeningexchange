using ScreeningExchange.Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.App.Api.Features.Studenties.UpsertStudent;

public static class DependencyBuilder
{
    public static IServiceCollection AddStudentUpsertStudentFeature(this IServiceCollection services)
    {
        services.AddTransient<IUseCaseOutputPort<Result<UpsertStudentResponse>>, JsonPresenter>();
        services.AddTransient<IInputOutputPortUseCase<UpsertStudentdRequest, IUseCaseOutputPort<Result<UpsertStudentResponse>>, Result<UpsertStudentResponse>>, UseCase>();
        return services;
    }
}