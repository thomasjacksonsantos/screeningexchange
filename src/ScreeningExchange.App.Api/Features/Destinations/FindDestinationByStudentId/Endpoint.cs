using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.App.Api.Features.Shared.Auth;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Destinations.FindDestinationByStudentId;

public class Endpoint(IInputOutputPortUseCase<FindDestinationByStudentIdRequest, IUseCaseOutputPort<Result<FindDestinationByStudentIdResponse>>, Result<FindDestinationByStudentIdResponse>> useCase)
    : Endpoint<FindDestinationByStudentIdRequest, Result<FindDestinationByStudentIdResponse>>
{
    private readonly IInputOutputPortUseCase<FindDestinationByStudentIdRequest, IUseCaseOutputPort<Result<FindDestinationByStudentIdResponse>>, Result<FindDestinationByStudentIdResponse>> useCase = useCase;

    public override void Configure()
    {
        Get("api/v1/destination/find/{id}");
        // PreProcessor<AuthInterceptor<FindDestinationByStudentIdRequest>>();
        AllowAnonymous();
        Description(c => c.Accepts<FindDestinationByStudentIdRequest>()
            .Produces<FindDestinationByStudentIdResponse>()
            .ProducesProblem(400)
            .WithTags("Destinations")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(FindDestinationByStudentIdRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}