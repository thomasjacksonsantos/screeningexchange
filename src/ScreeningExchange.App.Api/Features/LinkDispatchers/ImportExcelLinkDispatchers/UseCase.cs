
using Microsoft.Extensions.Options;
using ScreeningExchange.Domain.Aggregates.LinkDispatchersAggregate;
using ScreeningExchange.Domain.Aggregates.QuestionsAggregate;
using ScreeningExchange.Infrastructure.Configuration;
using ScreeningExchange.Infrastructure.Core;
using ScreeningExchange.Infrastructure.DataAccess;
using ScreeningExchange.Infrastructure.IO;

namespace ScreeningExchange.App.Api.Features.LinkDisptachers.ImportExcelLinkDispatchers;

public class UseCase
(
    ILinkDispatcherRepository linkDispatcherRepository,
    IBuildQuestionRepository buildQuestionRepository,
    IExcelRead excelRead,
    IServiceBus serviceBus,
    IUnitOfWork unitOfWork,
    IUseCaseOutputPort<Result<ImportExcelLinkDispatchersResponse>> outputPort
)
    : IInputOutputPortUseCase<ImportExcelLinkDispatchersRequest, IUseCaseOutputPort<Result<ImportExcelLinkDispatchersResponse>>, Result<ImportExcelLinkDispatchersResponse>>
{
    private readonly ILinkDispatcherRepository linkDispatcherRepository = linkDispatcherRepository;
    private readonly IBuildQuestionRepository buildQuestionRepository = buildQuestionRepository;
    private readonly IExcelRead excelRead = excelRead;
    private readonly IServiceBus serviceBus = serviceBus;
    private readonly IUnitOfWork unitOfWork = unitOfWork;
    private readonly IUseCaseOutputPort<Result<ImportExcelLinkDispatchersResponse>> OutputPort = outputPort;

    public async ValueTask<Result<ImportExcelLinkDispatchersResponse>> Execute(
        ImportExcelLinkDispatchersRequest input,
        CancellationToken ct = default
    )
    {
        var customers = excelRead.Read<Customer>(
            new ExcelParams(
                input.File.OpenReadStream()
            )
        );

        BuildQuestion buildQuestion = await buildQuestionRepository.FindAsync(Ulid.Parse(input.BuildQuestionId));

        var linkDispatchers = new List<LinkDispatcher>();

        customers.ForEach(customer =>
        {
            var linkDispatcher = LinkDispatcher.Create(
                buildQuestion,
                customer,
                "/form"
            );

            linkDispatcher.EnableSendToEmail();
            linkDispatcher.EnableSendToWhatsapp();

            linkDispatchers.Add(linkDispatcher);
        });

        await linkDispatcherRepository.AddBatchAsync(linkDispatchers);

        var linkDispatchersSb = linkDispatchers.Select(c => new { LinkDispatcherId = c.Id.ToString() });

        await serviceBus.SendMultiAsync(linkDispatchersSb, "process-customer-link-dispatcher");
        await unitOfWork.SaveChangesAsync(ct);

        return OutputPort.Ok(
            new ImportExcelLinkDispatchersResponse(
                "Excel is proccessing in brackground."
            )
        );
    }
}
