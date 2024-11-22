using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Questions.FindQuestionsById;

public class JsonPresenter : IUseCaseOutputPort<Result<FindQuestionByIdResponse>>
{
    public Result<FindQuestionByIdResponse> BadRequest(string message)
    {
        return Result.Fail<FindQuestionByIdResponse>(
            message
        );
    }

    public Result<FindQuestionByIdResponse> Ok(FindQuestionByIdResponse response)
    {
        return Result.Ok<FindQuestionByIdResponse>(
            response
        );
    }
}