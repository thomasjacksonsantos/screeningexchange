
using Microsoft.EntityFrameworkCore;
using ScreeningExchange.Domain.Aggregates.AgentsAggregate;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.Infrastructure.DomainImplementation.Aggregates.AgentsAggregate;

public sealed class EfAgentRepository(EfUnitOfWork<ApplicationDbContext> unitOfWork)
    : EfUnitOfWorkAttachedRepository<ApplicationDbContext, Agent>(unitOfWork), IAgentRepository
{
    public async ValueTask AddAsync(Agent agent)
    {
        Add(agent);
        await ValueTask.CompletedTask;
    }

    public async ValueTask<Agent> FindAsync(Ulid id)
    {
        return await Queryable()
            .FindAsync(id) ?? null!;
    }

    public async ValueTask<Agent> FindByEmailAsnyc(string email)
    {
        return await Queryable()
            .FirstAsync(c => c.Email.Value == email.ToLower());
    }

    public async ValueTask<IEnumerable<Agent>> FindAllAsync(
        int page,
        int total
    )
    {
        if (total <= 0 || total > 20)
            total = 20;

        if (page > 0)
            page = (page - 1) * total;

        return await Queryable()
            .OrderBy(c => c.Name)
            .Skip(page)
            .Take(total).ToListAsync();
    }
}