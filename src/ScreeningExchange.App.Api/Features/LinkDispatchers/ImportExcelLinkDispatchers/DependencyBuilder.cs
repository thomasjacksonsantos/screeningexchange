using ScreeningExchange.Infrastructure.Core;
using Microsoft.Extensions.DependencyInjection;

namespace ScreeningExchange.App.Api.Features.LinkDisptachers.ImportExcelLinkDispatchers;

public static class DependencyBuilder
{
    public static IServiceCollection AddImportExcelLinkDispatchersFeature(this IServiceCollection services)
    {
        services.AddTransient<IUseCaseOutputPort<Result<ImportExcelLinkDispatchersResponse>>, JsonPresenter>();
        services.AddTransient<IInputOutputPortUseCase<ImportExcelLinkDispatchersRequest, IUseCaseOutputPort<Result<ImportExcelLinkDispatchersResponse>>, Result<ImportExcelLinkDispatchersResponse>>, UseCase>();
        return services;
    }
}