
using ScreeningExchange.Domain.Aggregates.QuestionsAggregate;
using ScreeningExchange.Domain.Aggregates.ValueObjects;
using ScreeningExchange.Infrastructure.Core;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.App.Api.Features.Questions.UpsertQuestion;

public class UseCase
(
    IBuildQuestionRepository buildQuestionRepository,
    IUnitOfWork unitOfWork,
    IUseCaseOutputPort<Result<UpsertQuestionResponse>> outputPort
)
    : IInputOutputPortUseCase<UpsertQuestiondRequest, IUseCaseOutputPort<Result<UpsertQuestionResponse>>, Result<UpsertQuestionResponse>>
{
    private readonly IBuildQuestionRepository buildQuestionRepository = buildQuestionRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IUseCaseOutputPort<Result<UpsertQuestionResponse>> OutputPort = outputPort;

    public async ValueTask<Result<UpsertQuestionResponse>> Execute(
        UpsertQuestiondRequest input,
        CancellationToken ct = default
    )
    {
        var buildQuestion = BuildQuestion.Create();

        if (!string.IsNullOrWhiteSpace(input.Id))
        {
            buildQuestion = await buildQuestionRepository.FindAsync(Ulid.Parse(input.Id));

            if (buildQuestion is null)
                return OutputPort.BadRequest("Not found!");

            buildQuestion.CleanAll();
        }

        foreach (var question in input.Questions)
        {
            buildQuestion.AddQuestion(
                question.Id,
                Question.Create(
                    TextQuestion.Create(question.Text),
                    question.Awnsers
                )
            );
        }

        foreach (var flow in input.Flows)
        {
            buildQuestion.DefinirFluxo(
                flow.questionId,
                flow.Awnser,
                flow.NextQuestionId
            );
        }

        if (input.SendToEmail)
            buildQuestion.EnableSendToEmail();

        if (input.SendToWhatsApp)
            buildQuestion.EnableSendToWhatsApp();

        await buildQuestionRepository.AddAsync(buildQuestion);

        await unitOfWork.SaveChangesAsync(ct);

        return OutputPort.Ok(new UpsertQuestionResponse(
                "Questions build with success."
            )
        );
    }
}
