
using ScreeningExchange.Domain.Aggregates.SchoolsAggregate;
using ScreeningExchange.Domain.Aggregates.ValueObjects;
using ScreeningExchange.Infrastructure.Core;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.App.Api.Features.Schools.UpsertSchool;

public class UseCase
(
    ISchoolRepository schoolRepository,
    IUnitOfWork unitOfWork,
    IUseCaseOutputPort<Result<UpsertSchoolResponse>> outputPort
)
    : IInputOutputPortUseCase<UpsertSchoolRequest, IUseCaseOutputPort<Result<UpsertSchoolResponse>>, Result<UpsertSchoolResponse>>
{
    private readonly ISchoolRepository schoolRepository = schoolRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IUseCaseOutputPort<Result<UpsertSchoolResponse>> OutputPort = outputPort;

    public async ValueTask<Result<UpsertSchoolResponse>> Execute(
        UpsertSchoolRequest input,
        CancellationToken ct = default
    )
    {
        if (!string.IsNullOrWhiteSpace(input.Id))
        {
            var school = await schoolRepository.FindAsync(Ulid.Parse(input.Id));

            if (school is null)
                return OutputPort.BadRequest("School not found!");

            school.Update(
                Name.Create(input.SchoolName),
                Email.Create(input.SchoolEmail),
                Phone.Create(input.Phone)
            );

            await unitOfWork.SaveChangesAsync(ct);

            return OutputPort.Ok(new UpsertSchoolResponse(
                    input.Id,
                    "School updated with success."
                )
            );
        }
        else
        {
            var school = School.Create(
                Name.Create(input.SchoolName),
                Email.Create(input.SchoolEmail),
                Phone.Create(input.Phone),
                input.Uid!
            );

            await schoolRepository.AddAsync(school);

            await unitOfWork.SaveChangesAsync(ct);

            return OutputPort.Ok(new UpsertSchoolResponse(
                    school.Id.ToString(),
                    "School created with success."
                )
            );
        }

    }
}
