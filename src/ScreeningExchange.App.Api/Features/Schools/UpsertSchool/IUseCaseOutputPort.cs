
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Schools.UpsertSchool;

public interface IUseCaseOutputPort<TResult> : IOutputPort<TResult>
{
    TResult Ok(UpsertSchoolResponse response);
    TResult BadRequest(string message);
}