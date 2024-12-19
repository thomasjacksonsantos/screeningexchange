
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Agents.UpsertAgent;

public interface IUseCaseOutputPort<TResult> : IOutputPort<TResult>
{
    TResult Ok(UpsertAgentResponse response);
    TResult BadRequest(string message);
}