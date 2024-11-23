using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Destinations.UpsertDestination;

public class JsonPresenter : IUseCaseOutputPort<Result<UpsertDestinationResponse>>
{
    public Result<UpsertDestinationResponse> BadRequest(string message)
    {
        return Result.Fail<UpsertDestinationResponse>(
            message
        );
    }

    public Result<UpsertDestinationResponse> Ok(UpsertDestinationResponse response)
    {
        return Result.Ok<UpsertDestinationResponse>(
            response
        );
    }
}