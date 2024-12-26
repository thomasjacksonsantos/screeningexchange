namespace ScreeningExchange.Infrastructure.IO;

public interface IServiceBus
{
    Task SendAsync<TData>(TData data, string queueOrTopicName, string? session = null) where TData : class;
    Task<long> ScheduleAsync<TData>(TData data, string queueOrTopicName, DateTime date, string? session = null) where TData : class;
    Task SendMultiAsync<TData>(IEnumerable<TData> dataList, string queueOrTopicName, int lotAmount = 300) where TData : class;
    Task<long> RescheduleAsync<TData>(TData data, string queueOrTopicName, DateTime date,
        long sequenceNumber, string? session = null)
        where TData : class;
}