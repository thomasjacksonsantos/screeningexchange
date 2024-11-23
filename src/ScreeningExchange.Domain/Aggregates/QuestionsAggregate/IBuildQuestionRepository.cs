namespace ScreeningExchange.Domain.Aggregates.QuestionsAggregate;

public interface IBuildQuestionRepository
{
    ValueTask AddAsync(BuildQuestion buildQuestion);
    ValueTask<BuildQuestion> FindAsync(Ulid id);
    ValueTask<IEnumerable<BuildQuestion>> FindAllAsync();
}