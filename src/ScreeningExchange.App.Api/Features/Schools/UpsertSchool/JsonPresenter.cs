using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Schools.UpsertSchool;

public class JsonPresenter : IUseCaseOutputPort<Result<UpsertSchoolResponse>>
{
    public Result<UpsertSchoolResponse> BadRequest(string message)
    {
        return Result.Fail<UpsertSchoolResponse>(
            message
        );
    }

    public Result<UpsertSchoolResponse> Ok(UpsertSchoolResponse response)
    {
        return Result.Ok<UpsertSchoolResponse>(
            response
        );
    }
}