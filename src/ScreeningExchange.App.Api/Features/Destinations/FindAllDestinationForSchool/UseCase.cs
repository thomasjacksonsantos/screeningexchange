
using ScreeningExchange.Domain.Aggregates.DestinationsAggregate;
using ScreeningExchange.Infrastructure.Core;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.App.Api.Features.Destinations.FindAllDestinationForSchool;

public class UseCase
(
    IDestinationRepository destinationRepository,
    IUnitOfWork unitOfWork,
    IUseCaseOutputPort<Result<FindAllDestinationForSchoolResponse>> outputPort
)
    : IInputOutputPortUseCase<FindAllDestinationForSchooldRequest, IUseCaseOutputPort<Result<FindAllDestinationForSchoolResponse>>, Result<FindAllDestinationForSchoolResponse>>
{
    private readonly IDestinationRepository destinationRepository = destinationRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IUseCaseOutputPort<Result<FindAllDestinationForSchoolResponse>> OutputPort = outputPort;

    public async ValueTask<Result<FindAllDestinationForSchoolResponse>> Execute(
        FindAllDestinationForSchooldRequest input,
        CancellationToken ct = default
    )
    {
        var destinations = await destinationRepository.FindAllAsync(
            input.Page,
            input.Total
        );


        return OutputPort.Ok(new FindAllDestinationForSchoolResponse(
                destinations.Select(c => 
                {
                    c.BuildQuestion.IniciarQuestion(c.QuestionId);
                    var question = c.BuildQuestion.ExibirQuestionAtual();
                    return new DestinationResponse(
                        new StudentResponse(
                            c.Student.Id.ToString(),
                            c.Student.Name.Value,
                            c.Student.Email.Value,
                            c.Student.Phone.Value
                        ),
                        new QuestionResponse(
                            question!.Text.Value
                        )
                    );
                }
            )
        ));
    }
}
