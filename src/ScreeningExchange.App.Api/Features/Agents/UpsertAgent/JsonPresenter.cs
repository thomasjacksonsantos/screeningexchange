using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Agents.UpsertAgent;

public class JsonPresenter : IUseCaseOutputPort<Result<UpsertAgentResponse>>
{
    public Result<UpsertAgentResponse> BadRequest(string message)
    {
        return Result.Fail<UpsertAgentResponse>(
            message
        );
    }

    public Result<UpsertAgentResponse> Ok(UpsertAgentResponse response)
    {
        return Result.Ok<UpsertAgentResponse>(
            response
        );
    }
}