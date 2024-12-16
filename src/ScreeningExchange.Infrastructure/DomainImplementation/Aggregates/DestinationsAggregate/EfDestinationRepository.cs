
using Microsoft.EntityFrameworkCore;
using ScreeningExchange.Domain.Aggregates.DestinationsAggregate;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.Infrastructure.DomainImplementation.Aggregates.DestinationAggretation;

public sealed class EfDestinationRepository(EfUnitOfWork<ApplicationDbContext> unitOfWork)
    : EfUnitOfWorkAttachedRepository<ApplicationDbContext, Destination>(unitOfWork), IDestinationRepository
{
    public async ValueTask AddAsync(Destination destination)
    {
        Add(destination);
        await ValueTask.CompletedTask;
    }

    public async ValueTask<Destination> FindAsync(Ulid id)
    {
        return await Queryable()
            .FindAsync(id) ?? null!;
    }

    public async ValueTask<IEnumerable<Destination>> FindAllAsync(
        int page, 
        int total
    )
    {
        if (total <= 0 || total > 20)
            total = 20;

        if (page > 0)
            page = (page - 1) * total;

        return await Queryable()
            .OrderByDescending(c => c.DateTimeFinished)
            .Skip(page)
            .Take(total).ToListAsync();
    }

    public async ValueTask<Destination> FindByStudentIdAsnyc(Ulid studentId)
    {
        return await Queryable()
            .FirstOrDefaultAsync(c => c.StudentId == studentId) ?? null!;
    }
}