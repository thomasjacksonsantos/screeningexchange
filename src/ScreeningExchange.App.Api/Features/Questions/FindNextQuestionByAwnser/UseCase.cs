
using ScreeningExchange.Domain.Aggregates.QuestionsAggregate;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Questions.FindNextQuestionByAwnser;

public class UseCase
(
    IBuildQuestionRepository buildQuestionRepository,
    IUseCaseOutputPort<Result<FindNextQuestionByAwnserResponse>> outputPort
)
    : IInputOutputPortUseCase<FindNextQuestionByAwnserRequest, IUseCaseOutputPort<Result<FindNextQuestionByAwnserResponse>>, Result<FindNextQuestionByAwnserResponse>>
{
    private readonly IBuildQuestionRepository buildQuestionRepository = buildQuestionRepository;
    private readonly IUseCaseOutputPort<Result<FindNextQuestionByAwnserResponse>> OutputPort = outputPort;

    public async ValueTask<Result<FindNextQuestionByAwnserResponse>> Execute(
        FindNextQuestionByAwnserRequest input,
        CancellationToken ct = default
    )
    {
        var b = await buildQuestionRepository.FindAsync(Ulid.Parse(input.BuildQuestionId));

        if (b is null)
            return OutputPort.BadRequest("QuestionId not found.");

        b.IniciarQuestion(input.QuestionId);

        var nextQuestion = b.Awnser(input.Awnser);

        if (nextQuestion is null)
            return OutputPort.Ok(
                new FindNextQuestionByAwnserResponse(
                    true
                )
            );

        return OutputPort.Ok(
            new FindNextQuestionByAwnserResponse(
                false,
                b.QuestionIdActual,
                nextQuestion.Text.Value,
                nextQuestion.Awnsers
            )
        );
    }
}
