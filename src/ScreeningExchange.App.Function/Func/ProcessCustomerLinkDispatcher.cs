using System;
using System.Text;
using System.Text.Unicode;
using System.Threading.Tasks;
using Azure.Messaging.ServiceBus;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;
using ScreeningExchange.Domain.Aggregates.LinkDispatchersAggregate;
using ScreeningExchange.Infrastructure.DataAccess;
using ScreeningExchange.Infrastructure.IO;

namespace ScreeningExchange.App.Function
{
    public class ProcessCustomerLinkDispatcher
    {
        private readonly IEmailSend emailSend;
        private readonly IWhatsapp whatsapp;
        private ILinkDispatcherRepository linkDispatcherRepository;
        private IUnitOfWork unitOfWork;

        private readonly ILogger<ProcessCustomerLinkDispatcher> logger;

        public ProcessCustomerLinkDispatcher(
            IEmailSend emailSend,
            IWhatsapp whatsapp,
            ILinkDispatcherRepository linkDispatcherRepository,
            IUnitOfWork unitOfWork,
            ILogger<ProcessCustomerLinkDispatcher> logger
        )
        {
            this.emailSend = emailSend;
            this.whatsapp = whatsapp;
            this.linkDispatcherRepository = linkDispatcherRepository;
            this.unitOfWork = unitOfWork;
            this.logger = logger;
        }

        [Function(nameof(ProcessCustomerLinkDispatcher))]
        public async Task Run(
            [ServiceBusTrigger("process-customer-link-dispatcher", Connection = "ServiceBusConnection")]
            ServiceBusReceivedMessage message,
            ServiceBusMessageActions messageActions,
            CancellationToken ct)
        {
            logger.LogInformation("Message ID: {id}", message.MessageId);
            
            var content = Encoding.UTF8.GetString(message.Body);

            var linkDispatcher = await linkDispatcherRepository.FindAsync(Ulid.Parse(content));

            if (linkDispatcher!.SendToEmail)
            {
                var emailResult = await emailSend.SendAsync(
                    new EmailParams(linkDispatcher.Customer.Email,
                    "Questionarios Intercambio",
                    "Clique no link e responda o questionario.")
                );

                if (emailResult.Success)
                    linkDispatcher.WasEmailSentSuccess();
                else
                    linkDispatcher.ErroSendingEmail();

                linkDispatcher.CreateLog(
                    Log.Create(
                        emailResult.Message,
                        emailResult.Success ? Domain.Enums.LogStatusEnum.Success : Domain.Enums.LogStatusEnum.Error
                    )
                );
            }

            if (linkDispatcher!.SendToWhatsApp)
            {
                var whatsappResult = await whatsapp.SendAsync(
                    new WhatsappParams(
                        long.Parse(linkDispatcher.Customer.Phone),
                        "Voce recebeu essa mensagem de teste"
                    )
                );

                if (string.IsNullOrEmpty(whatsappResult.ErrorMessage))
                    linkDispatcher.WasEmailSentSuccess();
                else
                    linkDispatcher.ErroSendingEmail();

                linkDispatcher.CreateLog(
                    Log.Create(
                       string.IsNullOrEmpty(whatsappResult.ErrorMessage) ? string.Empty : $"{whatsappResult.ErrorCode} - {whatsappResult.ErrorMessage}",
                        string.IsNullOrEmpty(whatsappResult.ErrorMessage) ? Domain.Enums.LogStatusEnum.Success : Domain.Enums.LogStatusEnum.Error
                    )
                );
            }

            await unitOfWork.SaveChangesAsync(ct);

            // Complete the message
            await messageActions.CompleteMessageAsync(message);
        }
    }
}
