using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Destinations.FindDestinationByStudentId;

public class JsonPresenter : IUseCaseOutputPort<Result<FindDestinationByStudentIdResponse>>
{
    public Result<FindDestinationByStudentIdResponse> BadRequest(string message)
    {
        return Result.Fail<FindDestinationByStudentIdResponse>(
            message
        );
    }

    public Result<FindDestinationByStudentIdResponse> Ok(FindDestinationByStudentIdResponse response)
    {
        return Result.Ok<FindDestinationByStudentIdResponse>(
            response
        );
    }
}