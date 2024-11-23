
using ScreeningExchange.Domain.Aggregates.QuestionsAggregate;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Questions.FindAllFlows;

public class UseCase
(
    IBuildQuestionRepository buildQuestionRepository,
    IUseCaseOutputPort<Result<FindAllFlowsResponse>> outputPort
)
    : IInputOutputPortUseCase<FindAllFlowsRequest, IUseCaseOutputPort<Result<FindAllFlowsResponse>>, Result<FindAllFlowsResponse>>
{
    private readonly IBuildQuestionRepository buildQuestionRepository = buildQuestionRepository;
    private readonly IUseCaseOutputPort<Result<FindAllFlowsResponse>> OutputPort = outputPort;

    public async ValueTask<Result<FindAllFlowsResponse>> Execute(
        FindAllFlowsRequest input,
        CancellationToken ct = default
    )
    {
        var b = await buildQuestionRepository.FindAsync(Ulid.Parse(input.BuildQuestionId));

        if (b is null)
            return OutputPort.BadRequest("QuestionId not found.");

        var flows = b.Flows.Select(c => new FlowResponse(
            c.QuestionId.ToString(),
            c.Awnser,
            c.NextQuestionId
        ));

        return OutputPort.Ok(
            new FindAllFlowsResponse(
                flows.ToList()
            )
        );
    }
}
