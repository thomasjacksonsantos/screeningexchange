using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Schools.FindAllSchool;

public class JsonPresenter : IUseCaseOutputPort<Result<FindAllSchoolResponse>>
{
    public Result<FindAllSchoolResponse> BadRequest(string message)
    {
        return Result.Fail<FindAllSchoolResponse>(
            message
        );
    }

    public Result<FindAllSchoolResponse> Ok(FindAllSchoolResponse response)
    {
        return Result.Ok<FindAllSchoolResponse>(
            response
        );
    }
}