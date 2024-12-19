
using ScreeningExchange.Domain.Aggregates.StudentiesAggregate;
using ScreeningExchange.Domain.Aggregates.ValueObjects;
using ScreeningExchange.Infrastructure.Core;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.App.Api.Features.Studenties.UpsertStudent;

public class UseCase
(
    IStudentRepository studentRepository,
    IUnitOfWork unitOfWork,
    IUseCaseOutputPort<Result<UpsertStudentResponse>> outputPort
)
    : IInputOutputPortUseCase<UpsertStudentRequest, IUseCaseOutputPort<Result<UpsertStudentResponse>>, Result<UpsertStudentResponse>>
{
    private readonly IStudentRepository studentRepository = studentRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IUseCaseOutputPort<Result<UpsertStudentResponse>> OutputPort = outputPort;

    public async ValueTask<Result<UpsertStudentResponse>> Execute(
        UpsertStudentRequest input,
        CancellationToken ct = default
    )
    {
        if (!string.IsNullOrWhiteSpace(input.Id))
        {
            var student = await studentRepository.FindAsync(Ulid.Parse(input.Id));

            if (student is null)
                return OutputPort.BadRequest("Student not found!");

            student.Update(
                Name.Create(input.Name),
                Email.Create(input.StudentEmail),
                Phone.Create(input.Phone)
            );

            await unitOfWork.SaveChangesAsync(ct);

            return OutputPort.Ok(new UpsertStudentResponse(
                    input.Id,
                    "Student updated with success."
                )
            );
        }
        else
        {
            var create = Student.Create(
                 Name.Create(input.Name),
                 Email.Create(input.StudentEmail),
                 Phone.Create(input.Phone)
             );

            await studentRepository.AddAsync(create);

            await unitOfWork.SaveChangesAsync(ct);

            return OutputPort.Ok(new UpsertStudentResponse(
                    create.Id.ToString(),
                    "Student created with success."
                )
            );
        }
    }
}
