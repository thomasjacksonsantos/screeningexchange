using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Questions.FindAllFlows;

public class JsonPresenter : IUseCaseOutputPort<Result<FindAllFlowsResponse>>
{
    public Result<FindAllFlowsResponse> BadRequest(string message)
    {
        return Result.Fail<FindAllFlowsResponse>(
            message
        );
    }

    public Result<FindAllFlowsResponse> Ok(FindAllFlowsResponse response)
    {
        return Result.Ok<FindAllFlowsResponse>(
            response
        );
    }
}