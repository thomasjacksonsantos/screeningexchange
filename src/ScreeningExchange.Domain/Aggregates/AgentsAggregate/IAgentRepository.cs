

namespace ScreeningExchange.Domain.Aggregates.AgentsAggregate;

public interface IAgentRepository
{
    ValueTask AddAsync(Agent agent);
    ValueTask<Agent> FindAsync(Ulid id);
    ValueTask<IEnumerable<Agent>> FindAllAsync(
        int page,
        int total
    );
}