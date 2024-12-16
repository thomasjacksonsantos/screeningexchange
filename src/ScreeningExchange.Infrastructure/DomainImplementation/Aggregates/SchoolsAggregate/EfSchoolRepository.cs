
using Microsoft.EntityFrameworkCore;
using ScreeningExchange.Domain.Aggregates.SchoolsAggregate;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.Infrastructure.DomainImplementation.Aggregates.SchoolsAggregate;

public sealed class EfSchoolRepository(EfUnitOfWork<ApplicationDbContext> unitOfWork)
    : EfUnitOfWorkAttachedRepository<ApplicationDbContext, School>(unitOfWork), ISchoolRepository
{
    public async ValueTask AddAsync(School school)
    {
        Add(school);
        await ValueTask.CompletedTask;
    }

    public async ValueTask<School> FindAsync(Ulid id)
    {
        return await Queryable()
            .FindAsync(id) ?? null!;
    }

    public async ValueTask<School> FindByEmailAsnyc(string email)
    {
        return await Queryable()
            .FirstAsync(c => c.Email.Value == email.ToLower());
    }

    public async ValueTask<IEnumerable<School>> FindAllAsync(
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