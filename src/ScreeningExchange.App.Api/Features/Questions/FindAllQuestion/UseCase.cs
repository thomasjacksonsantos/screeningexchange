
using ScreeningExchange.Domain.Aggregates.QuestionsAggregate;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Questions.FindAllQuestion;

public class UseCase
(
    IBuildQuestionRepository buildQuestionRepository,
    IUseCaseOutputPort<Result<FindAllQuestionResponse>> outputPort
)
    : IInputOutputPortUseCase<FindAllQuestionRequest, IUseCaseOutputPort<Result<FindAllQuestionResponse>>, Result<FindAllQuestionResponse>>
{
    private readonly IBuildQuestionRepository buildQuestionRepository = buildQuestionRepository;
    private readonly IUseCaseOutputPort<Result<FindAllQuestionResponse>> OutputPort = outputPort;

    public async ValueTask<Result<FindAllQuestionResponse>> Execute(
        FindAllQuestionRequest input,
        CancellationToken ct = default
    )
    {
        var b = await buildQuestionRepository.FindAllAsync();
        var allQuestions = b.Select(c => new AllQuestionResponse(
            c.Id.ToString(),
            c.Questions.Select(q => new QuestionResponse(q.Id, q.Question.Text.Value, q.Question.Awnsers)).ToList(),
            c.Flows.Select(c => new FlowResponse(c.QuestionId, c.Awnser, c.NextQuestionId)).ToList()
        ));

        return OutputPort.Ok(
            new FindAllQuestionResponse(
                allQuestions.ToList()
            )
        );
    }
}
