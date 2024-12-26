using System;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace ScreeningExchange.App.Function
{
    public class ProcessCustomerLinkDispatcher
    {
        private readonly ILogger<ProcessCustomerLinkDispatcher> _logger;

        public ProcessCustomerLinkDispatcher(ILogger<ProcessCustomerLinkDispatcher> logger)
        {
            _logger = logger;
        }

        [Function(nameof(ProcessCustomerLinkDispatcher))]
        public async Task Run(
            [ServiceBusTrigger("process-customer-link-dispatcher", Connection = "screeningexchangeservicebus_SERVICEBUS")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions)
        {
            _logger.LogInformation("Message ID: {id}", message.MessageId);
            _logger.LogInformation("Message Body: {body}", message.Body);
            _logger.LogInformation("Message Content-Type: {contentType}", message.ContentType);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
