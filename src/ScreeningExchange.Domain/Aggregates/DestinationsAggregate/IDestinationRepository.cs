

namespace ScreeningExchange.Domain.Aggregates.DestinationsAggregate;

public interface IDestinationRepository
{
    ValueTask AddAsync(Destination destination);
    ValueTask<Destination> FindAsync(Ulid id);
    ValueTask<Destination> FindByStudentIdAsnyc(Ulid studentId);
    ValueTask<IEnumerable<Destination>> FindAllAsync();
}