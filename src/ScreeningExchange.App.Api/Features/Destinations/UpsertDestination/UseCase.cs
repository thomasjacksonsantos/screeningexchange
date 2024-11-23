
using ScreeningExchange.Domain.Aggregates.DestinationsAggregate;
using ScreeningExchange.Domain.Aggregates.QuestionsAggregate;
using ScreeningExchange.Domain.Aggregates.StudentiesAggregate;
using ScreeningExchange.Infrastructure.Core;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.App.Api.Features.Destinations.UpsertDestination;

public class UseCase
(
    IStudentRepository studentRepository,
    IBuildQuestionRepository buildQuestionRepository,
    IDestinationRepository destinationRepository,
    IUnitOfWork unitOfWork,
    IUseCaseOutputPort<Result<UpsertDestinationResponse>> outputPort
)
    : IInputOutputPortUseCase<UpsertDestinationdRequest, IUseCaseOutputPort<Result<UpsertDestinationResponse>>, Result<UpsertDestinationResponse>>
{
    private readonly IStudentRepository studentRepository = studentRepository;
    private readonly IBuildQuestionRepository buildQuestionRepository = buildQuestionRepository;
    private readonly IDestinationRepository destinationRepository = destinationRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IUseCaseOutputPort<Result<UpsertDestinationResponse>> OutputPort = outputPort;

    public async ValueTask<Result<UpsertDestinationResponse>> Execute(
        UpsertDestinationdRequest input,
        CancellationToken ct = default
    )
    {
        if (!string.IsNullOrWhiteSpace(input.Id))
        {
            var d = await destinationRepository.FindAsync(Ulid.Parse(input.Id));

            if (d is null)
                return OutputPort.BadRequest("Destination not found!");

            d.Update(
                input.QuestionId,
                input.Awnser
            );
        }
        else
        {
            var student = await studentRepository.FindAsync(Ulid.Parse(input.StudentId));
            var buildQuestion = await buildQuestionRepository.FindAsync(Ulid.Parse(input.BuildQuestionId));

            if (buildQuestion is null)
                return OutputPort.BadRequest("BuildQuestionId not found.");

            await destinationRepository.AddAsync(Destination.Create(
                    student,
                    buildQuestion,
                    input.QuestionId,
                    input.Awnser
                )
            );
        }

        await unitOfWork.SaveChangesAsync(ct);

        return OutputPort.Ok(new UpsertDestinationResponse(
                "Destination created with success."
            )
        );
    }
}
