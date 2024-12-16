
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Schools.FindSchoolById;

public interface IUseCaseOutputPort<TResult> : IOutputPort<TResult>
{
    TResult Ok(FindSchoolByIdResponse response);
    TResult BadRequest(string message);
}