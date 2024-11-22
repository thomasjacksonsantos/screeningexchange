
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Questions.FindQuestionsById;

public interface IUseCaseOutputPort<TResult> : IOutputPort<TResult>
{
    TResult Ok(FindQuestionByIdResponse response);
    TResult BadRequest(string message);
}