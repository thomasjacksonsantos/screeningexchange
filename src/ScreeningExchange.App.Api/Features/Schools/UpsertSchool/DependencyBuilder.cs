using ScreeningExchange.Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.App.Api.Features.Schools.UpsertSchool;

public static class DependencyBuilder
{
    public static IServiceCollection AddStudentUpsertSchoolFeature(this IServiceCollection services)
    {
        services.AddTransient<IUseCaseOutputPort<Result<UpsertSchoolResponse>>, JsonPresenter>();
        services.AddTransient<IInputOutputPortUseCase<UpsertSchoolRequest, IUseCaseOutputPort<Result<UpsertSchoolResponse>>, Result<UpsertSchoolResponse>>, UseCase>();
        return services;
    }
}