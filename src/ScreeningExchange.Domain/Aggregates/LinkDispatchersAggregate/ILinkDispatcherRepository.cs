

namespace ScreeningExchange.Domain.Aggregates.LinkDispatchersAggregate;

public interface ILinkDispatcherRepository
{
    ValueTask AddBatchAsync(IEnumerable<LinkDispatcher> linkDispatchers); 
    ValueTask<LinkDispatcher> FindAsync(Ulid id);
    ValueTask<IEnumerable<LinkDispatcher>> FindAllAsync(
        int page,
        int total
    );
}