using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Questions.FindNextQuestionByAwnser;

public class JsonPresenter : IUseCaseOutputPort<Result<FindNextQuestionByAwnserResponse>>
{
    public Result<FindNextQuestionByAwnserResponse> BadRequest(string message)
    {
        return Result.Fail<FindNextQuestionByAwnserResponse>(
            message
        );
    }

    public Result<FindNextQuestionByAwnserResponse> Ok(FindNextQuestionByAwnserResponse response)
    {
        return Result.Ok<FindNextQuestionByAwnserResponse>(
            response
        );
    }
}