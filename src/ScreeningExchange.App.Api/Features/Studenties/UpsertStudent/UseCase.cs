
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
    : IInputOutputPortUseCase<UpsertStudentdRequest, IUseCaseOutputPort<Result<UpsertStudentResponse>>, Result<UpsertStudentResponse>>
{
    private readonly IStudentRepository studentRepository = studentRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IUseCaseOutputPort<Result<UpsertStudentResponse>> OutputPort = outputPort;

    public async ValueTask<Result<UpsertStudentResponse>> Execute(
        UpsertStudentdRequest input,
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

        return OutputPort.Ok(new UpsertStudentResponse(
                "Student create with success."
            )
        );
    }
}
