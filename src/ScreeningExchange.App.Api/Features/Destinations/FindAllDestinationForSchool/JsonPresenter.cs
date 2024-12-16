using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Destinations.FindAllDestinationForSchool;

public class JsonPresenter : IUseCaseOutputPort<Result<FindAllDestinationForSchoolResponse>>
{
    public Result<FindAllDestinationForSchoolResponse> BadRequest(string message)
    {
        return Result.Fail<FindAllDestinationForSchoolResponse>(
            message
        );
    }

    public Result<FindAllDestinationForSchoolResponse> Ok(FindAllDestinationForSchoolResponse response)
    {
        return Result.Ok<FindAllDestinationForSchoolResponse>(
            response
        );
    }
}