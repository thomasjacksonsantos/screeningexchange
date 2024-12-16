
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Destinations.FindAllDestinationForSchool;

public interface IUseCaseOutputPort<TResult> : IOutputPort<TResult>
{
    TResult Ok(FindAllDestinationForSchoolResponse response);
    TResult BadRequest(string message);
}