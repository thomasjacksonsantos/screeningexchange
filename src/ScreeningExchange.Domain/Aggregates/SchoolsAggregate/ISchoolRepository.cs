


namespace ScreeningExchange.Domain.Aggregates.SchoolsAggregate;

public interface ISchoolRepository
{
    ValueTask AddAsync(School school);
    ValueTask<School> FindAsync(Ulid id);
    ValueTask<School> FindByEmailAsnyc(string email);
    ValueTask<IEnumerable<School>> FindAllAsync(
        int page,
        int total
    );
}