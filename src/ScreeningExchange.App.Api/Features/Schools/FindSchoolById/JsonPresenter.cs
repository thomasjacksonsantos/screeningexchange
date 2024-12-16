using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Schools.FindSchoolById;

public class JsonPresenter : IUseCaseOutputPort<Result<FindSchoolByIdResponse>>
{
    public Result<FindSchoolByIdResponse> BadRequest(string message)
    {
        return Result.Fail<FindSchoolByIdResponse>(
            message
        );
    }

    public Result<FindSchoolByIdResponse> Ok(FindSchoolByIdResponse response)
    {
        return Result.Ok<FindSchoolByIdResponse>(
            response
        );
    }
}