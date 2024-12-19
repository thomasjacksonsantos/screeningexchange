
using ScreeningExchange.Domain.Aggregates.AgentsAggregate;
using ScreeningExchange.Domain.Aggregates.ValueObjects;
using ScreeningExchange.Infrastructure.Core;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.App.Api.Features.Agents.UpsertAgent;

public class UseCase
(
    IAgentRepository agentRepository,
    IUnitOfWork unitOfWork,
    IUseCaseOutputPort<Result<UpsertAgentResponse>> outputPort
)
    : IInputOutputPortUseCase<UpsertAgentRequest, IUseCaseOutputPort<Result<UpsertAgentResponse>>, Result<UpsertAgentResponse>>
{
    private readonly IAgentRepository agentRepository = agentRepository;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IUseCaseOutputPort<Result<UpsertAgentResponse>> OutputPort = outputPort;

    public async ValueTask<Result<UpsertAgentResponse>> Execute(
        UpsertAgentRequest input,
        CancellationToken ct = default
    )
    {
        if (!string.IsNullOrWhiteSpace(input.Id))
        {
            var agent = await agentRepository.FindAsync(Ulid.Parse(input.Id));

            if (agent is null)
                return OutputPort.BadRequest("Agent not found!");

            agent.Update(
                Name.Create(input.Name),
                Email.Create(input.AgentEmail),
                Phone.Create(input.Phone)
            );

            await unitOfWork.SaveChangesAsync(ct);

            return OutputPort.Ok(new UpsertAgentResponse(
                    input.Id,
                    "Agent updated with success."

                )
            );
        }
        else
        {
            var create = Agent.Create(
                input.Uid!,
                 Name.Create(input.Name),
                 Email.Create(input.AgentEmail),
                 Phone.Create(input.Phone)
             );

            await agentRepository.AddAsync(create);

            await unitOfWork.SaveChangesAsync(ct);

            return OutputPort.Ok(new UpsertAgentResponse(
                    create.Id.ToString(),
                    "Agent processed with success."
                )
            );
        }

    }
}
