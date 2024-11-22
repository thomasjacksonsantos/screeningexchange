using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Questions.UpsertQuestion;

public class JsonPresenter : IUseCaseOutputPort<Result<UpsertQuestionResponse>>
{
    public Result<UpsertQuestionResponse> BadRequest(string message)
    {
        return Result.Fail<UpsertQuestionResponse>(
            message
        );
    }

    public Result<UpsertQuestionResponse> Ok(UpsertQuestionResponse response)
    {
        return Result.Ok<UpsertQuestionResponse>(
            response
        );
    }
}