
using ScreeningExchange.Domain.Aggregates.QuestionsAggregate;
using ScreeningExchange.Infrastructure.DataAccess;

namespace ScreeningExchange.Infrastructure.DomainImplementation.Aggregates.AlertAggregate;

public sealed class EfBuildQuestionRepository(EfUnitOfWork<ApplicationDbContext> unitOfWork)
    : EfUnitOfWorkAttachedRepository<ApplicationDbContext, BuildQuestion>(unitOfWork), IBuildQuestionRepository
{
    public ValueTask AddAsync(BuildQuestion buildQuestion)
    {
        throw new NotImplementedException();
    }

    public async ValueTask<BuildQuestion> FindAsync(Ulid id)
    {
        return await Queryable()
            .FindAsync(id) ?? null!;
    }
}