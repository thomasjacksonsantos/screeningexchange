
using Microsoft.EntityFrameworkCore;
using ScreeningExchange.Domain.Aggregates.StudentiesAggregate;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.Infrastructure.DomainImplementation.Aggregates.StudentiesAggregate;

public sealed class EfStudentRepository(EfUnitOfWork<ApplicationDbContext> unitOfWork)
    : EfUnitOfWorkAttachedRepository<ApplicationDbContext, Student>(unitOfWork), IStudentRepository
{
    public async ValueTask AddAsync(Student student)
    {
        Add(student);
        await ValueTask.CompletedTask;
    }

    public async ValueTask<Student> FindAsync(Ulid id)
    {
        return await Queryable()
            .FindAsync(id) ?? null!;
    }

    public async ValueTask<Student> FindByEmailAsnyc(string email)
    {
        return await Queryable()
            .FirstAsync(c => c.Email.Value == email.ToLower());
    }

    public async ValueTask<IEnumerable<Student>> FindAllAsync()
    {
        return await Queryable().ToListAsync();
    }
}