
using Microsoft.EntityFrameworkCore;
using ScreeningExchange.Domain.Aggregates.LinkDispatchersAggregate;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.Infrastructure.DomainImplementation.Aggregates.LinkDispatchersAggregate;

public sealed class EfLinkDispatcherRepository(EfUnitOfWork<ApplicationDbContext> unitOfWork)
    : EfUnitOfWorkAttachedRepository<ApplicationDbContext, LinkDispatcher>(unitOfWork), ILinkDispatcherRepository
{
    public async ValueTask AddBatchAsync(IEnumerable<LinkDispatcher> linkDispatchers)
    {
        await Queryable().AddRangeAsync(linkDispatchers);
    }

    public async ValueTask<IEnumerable<LinkDispatcher>> FindAllAsync(int page, int total)
    {
        if (total <= 0 || total > 20)
            total = 20;

        if (page > 0)
            page = (page - 1) * total;

        return await Queryable()
            .OrderBy(c => c.CreatedOn)
            .Skip(page)
            .Take(total).ToListAsync();
    }

    public async ValueTask<LinkDispatcher> FindAsync(Ulid id)
    {
        return await Queryable()
            .FindAsync(id) ?? null!;
    }
}