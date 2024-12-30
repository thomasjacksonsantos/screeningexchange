using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.App.Api.Features.Shared.Auth;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.LinkDisptachers.ImportExcelLinkDispatchers;

public class Endpoint(IInputOutputPortUseCase<ImportExcelLinkDispatchersRequest, IUseCaseOutputPort<Result<ImportExcelLinkDispatchersResponse>>, Result<ImportExcelLinkDispatchersResponse>> useCase)
    : Endpoint<ImportExcelLinkDispatchersRequest, Result<ImportExcelLinkDispatchersResponse>>
{
    private readonly IInputOutputPortUseCase<ImportExcelLinkDispatchersRequest, IUseCaseOutputPort<Result<ImportExcelLinkDispatchersResponse>>, Result<ImportExcelLinkDispatchersResponse>> useCase = useCase;

    public override void Configure()
    {
        Post("api/v1/link-dispatchers/import/excel");
        // PreProcessor<AuthInterceptor<ImportExcelLinkDispatchersRequest>>();
        AllowAnonymous();
        AllowFileUploads();
        Description(c => c.Accepts<ImportExcelLinkDispatchersRequest>()
            .Produces<ImportExcelLinkDispatchersResponse>()
            .ProducesProblem(400)
            .WithTags("Link Dispatchers")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(ImportExcelLinkDispatchersRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}