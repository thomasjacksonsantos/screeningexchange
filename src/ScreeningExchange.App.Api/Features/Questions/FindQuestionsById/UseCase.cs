
using ScreeningExchange.Domain.Aggregates.QuestionsAggregate;
using ScreeningExchange.Infrastructure.Core;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.App.Api.Features.Questions.FindQuestionById;

public class UseCase
(
    IBuildQuestionRepository buildQuestionRepository,
    IUnitOfWork unitOfWork,
    IUseCaseOutputPort<Result<FindQuestionByIdResponse>> outputPort
)
    : IInputOutputPortUseCase<FindQuestionByIdRequest, IUseCaseOutputPort<Result<FindQuestionByIdResponse>>, Result<FindQuestionByIdResponse>>
{
    private readonly IBuildQuestionRepository buildQuestionRepository = buildQuestionRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IUseCaseOutputPort<Result<FindQuestionByIdResponse>> OutputPort = outputPort;

    public async ValueTask<Result<FindQuestionByIdResponse>> Execute(
        FindQuestionByIdRequest input,
        CancellationToken ct = default
    )
    {
        var b = await buildQuestionRepository.FindAsync(Ulid.Parse(input.BuildQuestionId));

        if (b is null)
            return OutputPort.BadRequest("QuestionId not found.");

        b.IniciarQuestion(input.QuestionId);
        var q = b.ExibirQuestionAtual();

        if (q is null)
            return OutputPort.Ok(
                new FindQuestionByIdResponse(
                    true,
                    null,
                    null
                )
            );

        return OutputPort.Ok(
            new FindQuestionByIdResponse(
                false,
                q.Text.Value,
                q.Awnsers
            )
        );
    }
}
