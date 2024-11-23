using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.App.Api.Features.Shared.Auth;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Destinations.UpsertDestination;

public class Endpoint(IInputOutputPortUseCase<UpsertDestinationdRequest, IUseCaseOutputPort<Result<UpsertDestinationResponse>>, Result<UpsertDestinationResponse>> useCase)
    : Endpoint<UpsertDestinationdRequest, Result<UpsertDestinationResponse>>
{
    private readonly IInputOutputPortUseCase<UpsertDestinationdRequest, IUseCaseOutputPort<Result<UpsertDestinationResponse>>, Result<UpsertDestinationResponse>> useCase = useCase;

    public override void Configure()
    {
        Put("api/v1/destination");
        // PreProcessor<AuthInterceptor<UpsertDestinationdRequest>>();
        AllowAnonymous();
        Description(c => c.Accepts<UpsertDestinationResponse>()
            .Produces<UpsertDestinationResponse>()
            .ProducesProblem(400)
            .WithTags("Destinations")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(UpsertDestinationdRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}