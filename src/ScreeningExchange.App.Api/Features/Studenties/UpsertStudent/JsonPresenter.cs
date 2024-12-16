using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Studenties.UpsertStudent;

public class JsonPresenter : IUseCaseOutputPort<Result<UpsertStudentResponse>>
{
    public Result<UpsertStudentResponse> BadRequest(string message)
    {
        return Result.Fail<UpsertStudentResponse>(
            message
        );
    }

    public Result<UpsertStudentResponse> Ok(UpsertStudentResponse response)
    {
        return Result.Ok<UpsertStudentResponse>(
            response
        );
    }
}