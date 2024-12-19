using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.App.Api.Features.Shared.Auth;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Agents.FindAgentById;

public class Endpoint(IInputOutputPortUseCase<FindAgentByIdRequest, IUseCaseOutputPort<Result<FindAgentByIdResponse>>, Result<FindAgentByIdResponse>> useCase)
    : Endpoint<FindAgentByIdRequest, Result<FindAgentByIdResponse>>
{
    private readonly IInputOutputPortUseCase<FindAgentByIdRequest, IUseCaseOutputPort<Result<FindAgentByIdResponse>>, Result<FindAgentByIdResponse>> useCase = useCase;

    public override void Configure()
    {
        Get("api/v1/agent/find/{id}");
        PreProcessor<AuthInterceptor<FindAgentByIdRequest>>();
        Description(c => c.Accepts<FindAgentByIdRequest>()
            .Produces<FindAgentByIdResponse>()
            .ProducesProblem(400)
            .WithTags("Agents")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(FindAgentByIdRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}