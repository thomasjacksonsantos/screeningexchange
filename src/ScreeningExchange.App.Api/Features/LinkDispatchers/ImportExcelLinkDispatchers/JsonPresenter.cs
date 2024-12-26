using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.LinkDisptachers.ImportExcelLinkDispatchers;

public class JsonPresenter : IUseCaseOutputPort<Result<ImportExcelLinkDispatchersResponse>>
{
    public Result<ImportExcelLinkDispatchersResponse> BadRequest(string message)
    {
        return Result.Fail<ImportExcelLinkDispatchersResponse>(
            message
        );
    }

    public Result<ImportExcelLinkDispatchersResponse> Ok(ImportExcelLinkDispatchersResponse response)
    {
        return Result.Ok<ImportExcelLinkDispatchersResponse>(
            response
        );
    }
}