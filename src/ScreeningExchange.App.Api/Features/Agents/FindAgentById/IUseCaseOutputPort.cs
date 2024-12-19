
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Agents.FindAgentById;

public interface IUseCaseOutputPort<TResult> : IOutputPort<TResult>
{
    TResult Ok(FindAgentByIdResponse response);
    TResult BadRequest(string message);
}