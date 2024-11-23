
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Questions.FindNextQuestionByAwnser;

public interface IUseCaseOutputPort<TResult> : IOutputPort<TResult>
{
    TResult Ok(FindNextQuestionByAwnserResponse response);
    TResult BadRequest(string message);
}