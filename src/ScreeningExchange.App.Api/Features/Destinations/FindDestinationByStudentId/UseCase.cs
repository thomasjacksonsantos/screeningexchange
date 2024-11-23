
using ScreeningExchange.Domain.Aggregates.DestinationsAggregate;
using ScreeningExchange.Infrastructure.Core;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.App.Api.Features.Destinations.FindDestinationByStudentId;

public class UseCase
(
    IDestinationRepository destinationRepository,
    IUnitOfWork unitOfWork,
    IUseCaseOutputPort<Result<FindDestinationByStudentIdResponse>> outputPort
)
    : IInputOutputPortUseCase<FindDestinationByStudentIddRequest, IUseCaseOutputPort<Result<FindDestinationByStudentIdResponse>>, Result<FindDestinationByStudentIdResponse>>
{
    private readonly IDestinationRepository destinationRepository = destinationRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IUseCaseOutputPort<Result<FindDestinationByStudentIdResponse>> OutputPort = outputPort;

    public async ValueTask<Result<FindDestinationByStudentIdResponse>> Execute(
        FindDestinationByStudentIddRequest input,
        CancellationToken ct = default
    )
    {
        var d = await destinationRepository.FindByStudentIdAsnyc(Ulid.Parse(input.Id));


        return OutputPort.Ok(new FindDestinationByStudentIdResponse(
                d.QuestionId,
                d.Awnser
            )
        );
    }
}
