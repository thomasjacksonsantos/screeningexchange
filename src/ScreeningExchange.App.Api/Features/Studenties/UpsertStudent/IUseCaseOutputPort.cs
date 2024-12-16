
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Studenties.UpsertStudent;

public interface IUseCaseOutputPort<TResult> : IOutputPort<TResult>
{
    TResult Ok(UpsertStudentResponse response);
    TResult BadRequest(string message);
}