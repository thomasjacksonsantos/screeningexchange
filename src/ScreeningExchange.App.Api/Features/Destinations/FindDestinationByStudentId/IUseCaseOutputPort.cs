
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Destinations.FindDestinationByStudentId;

public interface IUseCaseOutputPort<TResult> : IOutputPort<TResult>
{
    TResult Ok(FindDestinationByStudentIdResponse response);
    TResult BadRequest(string message);
}