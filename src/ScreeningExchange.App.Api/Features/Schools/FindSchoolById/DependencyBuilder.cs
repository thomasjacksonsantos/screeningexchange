using ScreeningExchange.Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.App.Api.Features.Schools.FindSchoolById;

public static class DependencyBuilder
{
    public static IServiceCollection AddStudentFindSchoolByIdFeature(this IServiceCollection services)
    {
        services.AddTransient<IUseCaseOutputPort<Result<FindSchoolByIdResponse>>, JsonPresenter>();
        services.AddTransient<IInputOutputPortUseCase<FindSchoolByIdRequest, IUseCaseOutputPort<Result<FindSchoolByIdResponse>>, Result<FindSchoolByIdResponse>>, UseCase>();
        return services;
    }
}