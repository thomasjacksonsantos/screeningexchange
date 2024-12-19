using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.App.Api.Features.Shared.Auth;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Destinations.FindAllDestinationForSchool;

public class Endpoint(IInputOutputPortUseCase<FindAllDestinationForSchoolIdRequest, IUseCaseOutputPort<Result<FindAllDestinationForSchoolResponse>>, Result<FindAllDestinationForSchoolResponse>> useCase)
    : Endpoint<FindAllDestinationForSchoolIdRequest, Result<FindAllDestinationForSchoolResponse>>
{
    private readonly IInputOutputPortUseCase<FindAllDestinationForSchoolIdRequest, IUseCaseOutputPort<Result<FindAllDestinationForSchoolResponse>>, Result<FindAllDestinationForSchoolResponse>> useCase = useCase;

    public override void Configure()
    {
        Get("api/v1/destination/school/all");
        // PreProcessor<AuthInterceptor<FindAllDestinationForSchoolIdRequest>>();
        AllowAnonymous();
        Description(c => c.Accepts<FindAllDestinationForSchoolIdRequest>()
            .Produces<FindAllDestinationForSchoolResponse>()
            .ProducesProblem(400)
            .WithTags("Destinations")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(FindAllDestinationForSchoolIdRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}