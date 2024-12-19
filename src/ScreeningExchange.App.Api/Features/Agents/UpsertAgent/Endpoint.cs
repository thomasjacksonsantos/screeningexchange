using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.App.Api.Features.Shared.Auth;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Agents.UpsertAgent;

public class Endpoint(IInputOutputPortUseCase<UpsertAgentRequest, IUseCaseOutputPort<Result<UpsertAgentResponse>>, Result<UpsertAgentResponse>> useCase)
    : Endpoint<UpsertAgentRequest, Result<UpsertAgentResponse>>
{
    private readonly IInputOutputPortUseCase<UpsertAgentRequest, IUseCaseOutputPort<Result<UpsertAgentResponse>>, Result<UpsertAgentResponse>> useCase = useCase;

    public override void Configure()
    {
        Put("api/v1/agent");
        PreProcessor<AuthInterceptor<UpsertAgentRequest>>();
        Description(c => c.Accepts<UpsertAgentRequest>()
            .Produces<UpsertAgentResponse>()
            .ProducesProblem(400)
            .WithTags("Agents")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(UpsertAgentRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}