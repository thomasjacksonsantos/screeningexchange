using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.App.Api.Features.Shared.Auth;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Destinations.FindDestinationByStudentId;

public class Endpoint(IInputOutputPortUseCase<FindDestinationByStudentIddRequest, IUseCaseOutputPort<Result<FindDestinationByStudentIdResponse>>, Result<FindDestinationByStudentIdResponse>> useCase)
    : Endpoint<FindDestinationByStudentIddRequest, Result<FindDestinationByStudentIdResponse>>
{
    private readonly IInputOutputPortUseCase<FindDestinationByStudentIddRequest, IUseCaseOutputPort<Result<FindDestinationByStudentIdResponse>>, Result<FindDestinationByStudentIdResponse>> useCase = useCase;

    public override void Configure()
    {
        Get("api/v1/destination/find/{id}");
        // PreProcessor<AuthInterceptor<FindDestinationByStudentIddRequest>>();
        AllowAnonymous();
        Description(c => c.Accepts<FindDestinationByStudentIdResponse>()
            .Produces<FindDestinationByStudentIdResponse>()
            .ProducesProblem(400)
            .WithTags("Destinations")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(FindDestinationByStudentIddRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}