
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

    public async ValueTask<IEnumerable<Destination>> FindAllAsync()
    {
        return await Queryable().ToListAsync();
    }

    public async ValueTask<Destination> FindByStudentIdAsnyc(Ulid studentId)
    {
        return await Queryable()
            .FirstOrDefaultAsync(c => c.StudentId == studentId) ?? null!;
    }
}