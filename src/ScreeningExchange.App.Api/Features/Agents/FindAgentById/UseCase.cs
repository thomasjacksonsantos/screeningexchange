
using ScreeningExchange.Domain.Aggregates.AgentsAggregate;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Agents.FindAgentById;

public class UseCase
(
    IAgentRepository agentRepository,
    IUseCaseOutputPort<Result<FindAgentByIdResponse>> outputPort
)
    : IInputOutputPortUseCase<FindAgentByIdRequest, IUseCaseOutputPort<Result<FindAgentByIdResponse>>, Result<FindAgentByIdResponse>>
{
    private readonly IAgentRepository agentRepository = agentRepository;
    private readonly IUseCaseOutputPort<Result<FindAgentByIdResponse>> OutputPort = outputPort;

    public async ValueTask<Result<FindAgentByIdResponse>> Execute(
        FindAgentByIdRequest input,
        CancellationToken ct = default
    )
    {
        var agent = await agentRepository.FindAsync(Ulid.Parse(input.Id));

        if (agent is null)
            return OutputPort.BadRequest("Agent not found!");

        return OutputPort.Ok(new FindAgentByIdResponse(
                agent.Id.ToString(),
                agent.Name.Value,
                agent.Email.Value,
                agent.Phone.Value
            )
        );
    }
}
