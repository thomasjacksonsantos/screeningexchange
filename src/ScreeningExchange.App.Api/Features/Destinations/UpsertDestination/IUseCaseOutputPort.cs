
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Destinations.UpsertDestination;

public interface IUseCaseOutputPort<TResult> : IOutputPort<TResult>
{
    TResult Ok(UpsertDestinationResponse response);
    TResult BadRequest(string message);
}