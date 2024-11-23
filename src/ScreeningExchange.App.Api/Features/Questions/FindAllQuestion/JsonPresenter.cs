using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Questions.FindAllQuestion;

public class JsonPresenter : IUseCaseOutputPort<Result<FindAllQuestionResponse>>
{
    public Result<FindAllQuestionResponse> BadRequest(string message)
    {
        return Result.Fail<FindAllQuestionResponse>(
            message
        );
    }

    public Result<FindAllQuestionResponse> Ok(FindAllQuestionResponse response)
    {
        return Result.Ok<FindAllQuestionResponse>(
            response
        );
    }
}