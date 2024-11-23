
using Microsoft.EntityFrameworkCore;
using ScreeningExchange.Domain.Aggregates.QuestionsAggregate;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.Infrastructure.DomainImplementation.Aggregates.BuildQuestionsAggregate;

public sealed class EfBuildQuestionRepository(EfUnitOfWork<ApplicationDbContext> unitOfWork)
    : EfUnitOfWorkAttachedRepository<ApplicationDbContext, BuildQuestion>(unitOfWork), IBuildQuestionRepository
{
    public async ValueTask AddAsync(BuildQuestion buildQuestion)
    {
        Add(buildQuestion);
        await ValueTask.CompletedTask;
    }

    public async ValueTask<BuildQuestion> FindAsync(Ulid id)
    {
        return await Queryable()
            .FindAsync(id) ?? null!;
    }

    public async ValueTask<IEnumerable<BuildQuestion>> FindAllAsync()
    {
        return await Queryable().ToListAsync();
    }
}