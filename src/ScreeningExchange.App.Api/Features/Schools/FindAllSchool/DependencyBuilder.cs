using ScreeningExchange.Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.App.Api.Features.Schools.FindAllSchool;

public static class DependencyBuilder
{
    public static IServiceCollection AddStudentFindAllSchoolFeature(this IServiceCollection services)
    {
        services.AddTransient<IUseCaseOutputPort<Result<FindAllSchoolResponse>>, JsonPresenter>();
        services.AddTransient<IInputOutputPortUseCase<FindAllSchoolRequest, IUseCaseOutputPort<Result<FindAllSchoolResponse>>, Result<FindAllSchoolResponse>>, UseCase>();
        return services;
    }
}