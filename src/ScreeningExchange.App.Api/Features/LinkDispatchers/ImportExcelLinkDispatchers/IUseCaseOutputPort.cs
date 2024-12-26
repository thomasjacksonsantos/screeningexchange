
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.LinkDisptachers.ImportExcelLinkDispatchers;

public interface IUseCaseOutputPort<TResult> : IOutputPort<TResult>
{
    TResult Ok(ImportExcelLinkDispatchersResponse response);
    TResult BadRequest(string message);
}