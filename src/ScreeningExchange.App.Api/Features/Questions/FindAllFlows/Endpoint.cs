using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Questions.FindAllFlows;

public class Endpoint(IInputOutputPortUseCase<FindAllFlowsRequest, IUseCaseOutputPort<Result<FindAllFlowsResponse>>, Result<FindAllFlowsResponse>> useCase)
    : Endpoint<FindAllFlowsRequest, Result<FindAllFlowsResponse>>
{
    private readonly IInputOutputPortUseCase<FindAllFlowsRequest, IUseCaseOutputPort<Result<FindAllFlowsResponse>>, Result<FindAllFlowsResponse>> useCase = useCase;

    public override void Configure()
    {
        Get("api/v1/build-question/{buildquestionid}/flow/all");
        // PreProcessor<AuthInterceptor<FindAllFlowsRequest>>();
        AllowAnonymous();
        Description(c => c.Accepts<FindAllFlowsRequest>()
            .Produces<FindAllFlowsResponse>()
            .ProducesProblem(400)
            .WithTags("Questions")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(FindAllFlowsRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}