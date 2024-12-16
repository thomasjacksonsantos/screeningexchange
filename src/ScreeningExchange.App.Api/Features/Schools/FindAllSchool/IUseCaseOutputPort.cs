
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Schools.FindAllSchool;

public interface IUseCaseOutputPort<TResult> : IOutputPort<TResult>
{
    TResult Ok(FindAllSchoolResponse response);
    TResult BadRequest(string message);
}