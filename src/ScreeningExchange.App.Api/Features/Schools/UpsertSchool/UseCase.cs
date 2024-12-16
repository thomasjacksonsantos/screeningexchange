
using ScreeningExchange.Domain.Aggregates.StudentiesAggregate;
using ScreeningExchange.Domain.Aggregates.ValueObjects;
using ScreeningExchange.Infrastructure.Core;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.App.Api.Features.Schools.UpsertSchool;

public class UseCase
(
    IStudentRepository studentRepository,
    IUnitOfWork unitOfWork,
    IUseCaseOutputPort<Result<UpsertSchoolResponse>> outputPort
)
    : IInputOutputPortUseCase<UpsertSchoolRequest, IUseCaseOutputPort<Result<UpsertSchoolResponse>>, Result<UpsertSchoolResponse>>
{
    private readonly IStudentRepository studentRepository = studentRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IUseCaseOutputPort<Result<UpsertSchoolResponse>> OutputPort = outputPort;

    public async ValueTask<Result<UpsertSchoolResponse>> Execute(
        UpsertSchoolRequest input,
        CancellationToken ct = default
    )
    {
        if (!string.IsNullOrWhiteSpace(input.Id))
        {
            var student = await studentRepository.FindAsync(Ulid.Parse(input.Id));

            if (student is null)
                return OutputPort.BadRequest("School not found!");

            student.Update(
                Name.Create(input.Name),
                Email.Create(input.EmailStudent),
                Phone.Create(input.Phone)
            );
        }
        else
        {
            var create = Student.Create(
                 Name.Create(input.Name),
                 Email.Create(input.EmailStudent),
                 Phone.Create(input.Phone)
             );

            await studentRepository.AddAsync(create);
        }

        await unitOfWork.SaveChangesAsync(ct);

        return OutputPort.Ok(new UpsertSchoolResponse(
                "School create with success."
            )
        );
    }
}
