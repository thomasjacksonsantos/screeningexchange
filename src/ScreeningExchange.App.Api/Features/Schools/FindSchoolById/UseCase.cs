
using ScreeningExchange.Domain.Aggregates.SchoolsAggregate;
using ScreeningExchange.Infrastructure.Core;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.App.Api.Features.Schools.FindSchoolById;

public class UseCase
(
    ISchoolRepository schoolRepository,
    IUnitOfWork unitOfWork,
    IUseCaseOutputPort<Result<FindSchoolByIdResponse>> outputPort
)
    : IInputOutputPortUseCase<FindSchoolByIdRequest, IUseCaseOutputPort<Result<FindSchoolByIdResponse>>, Result<FindSchoolByIdResponse>>
{
    private readonly ISchoolRepository schoolRepository = schoolRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IUseCaseOutputPort<Result<FindSchoolByIdResponse>> OutputPort = outputPort;

    public async ValueTask<Result<FindSchoolByIdResponse>> Execute(
        FindSchoolByIdRequest input,
        CancellationToken ct = default
    )
    {
        var school = await schoolRepository.FindAsync(Ulid.Parse(input.Id));

        if (school is null)
            return OutputPort.BadRequest("School not found!");

        return OutputPort.Ok(new FindSchoolByIdResponse(
             school.Id.ToString(),
             school.Name.Value,
             school.Email.Value,
             school.Phone.Value
            )
        );
    }
}
