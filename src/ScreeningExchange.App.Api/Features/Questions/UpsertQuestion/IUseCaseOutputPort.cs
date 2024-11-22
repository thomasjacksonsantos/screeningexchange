
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Questions.UpsertQuestion;

public interface IUseCaseOutputPort<TResult> : IOutputPort<TResult>
{
    TResult Ok(UpsertQuestionResponse response);
    TResult BadRequest(string message);
}