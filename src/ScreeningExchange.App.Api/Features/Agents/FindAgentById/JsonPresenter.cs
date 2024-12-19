using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Agents.FindAgentById;

public class JsonPresenter : IUseCaseOutputPort<Result<FindAgentByIdResponse>>
{
    public Result<FindAgentByIdResponse> BadRequest(string message)
    {
        return Result.Fail<FindAgentByIdResponse>(
            message
        );
    }

    public Result<FindAgentByIdResponse> Ok(FindAgentByIdResponse response)
    {
        return Result.Ok<FindAgentByIdResponse>(
            response
        );
    }
}