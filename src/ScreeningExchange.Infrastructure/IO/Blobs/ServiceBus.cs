using Azure.Messaging.ServiceBus;
using System.Text.Json;

namespace ScreeningExchange.Infrastructure.IO.Blobs;

public class ServiceBus : IServiceBus
{
    private readonly ServiceBusClient _serviceBusClient;

    public ServiceBus(ServiceBusClient serviceBusClient)
    {
        _serviceBusClient = serviceBusClient;
    }

    public async Task SendAsync<TData>(TData data, string queueOrTopicName, string? session = null)
        where TData : class
    {
        var sender = _serviceBusClient.CreateSender(queueOrTopicName);

        var message = new ServiceBusMessage(BinaryData.FromObjectAsJson(data, new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        }))
        { SessionId = session };

        await sender.SendMessageAsync(message);
    }

    public async Task<long> ScheduleAsync<TData>(TData data, string queueOrTopicName, DateTime date, string? session = null)
        where TData : class
    {
        var sender = _serviceBusClient.CreateSender(queueOrTopicName);

        var message = new ServiceBusMessage(BinaryData.FromObjectAsJson(data, new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        }));

        message.SessionId = session;

        return await sender.ScheduleMessageAsync(message, date);
    }

    public async Task<long> RescheduleAsync<TData>(TData data, string queueOrTopicName, DateTime date, long sequenceNumber, string? session = null)
        where TData : class
    {
        var sender = _serviceBusClient.CreateSender(queueOrTopicName);

        var message = new ServiceBusMessage(BinaryData.FromObjectAsJson(data, new JsonSerializerOptions
        {
            WriteIndented = true,
            Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
        }));

        message.SessionId = session;

        await sender.CancelScheduledMessageAsync(sequenceNumber);

        return await sender.ScheduleMessageAsync(message, date);
    }

    public async Task SendMultiAsync<TData>(IEnumerable<TData> dataList, string queueOrTopicName, int lotAmount = 300)
        where TData : class
    {
        var sender = _serviceBusClient.CreateSender(queueOrTopicName);

        var total = dataList.Count();
        var sendTotal = 0;

        while (sendTotal < total)
        {
            var items = dataList
                .Skip(sendTotal)
                .Take(lotAmount)
                .Select(c => new ServiceBusMessage(BinaryData.FromObjectAsJson(c))
                {
                    SessionId = new Guid().ToString()
                })
                .ToList();

            await sender.SendMessagesAsync(items);

            sendTotal += lotAmount;
        }
    }
}