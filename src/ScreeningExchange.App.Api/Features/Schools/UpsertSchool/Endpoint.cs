using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.App.Api.Features.Shared.Auth;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Schools.UpsertSchool;

public class Endpoint(IInputOutputPortUseCase<UpsertSchoolRequest, IUseCaseOutputPort<Result<UpsertSchoolResponse>>, Result<UpsertSchoolResponse>> useCase)
    : Endpoint<UpsertSchoolRequest, Result<UpsertSchoolResponse>>
{
    private readonly IInputOutputPortUseCase<UpsertSchoolRequest, IUseCaseOutputPort<Result<UpsertSchoolResponse>>, Result<UpsertSchoolResponse>> useCase = useCase;

    public override void Configure()
    {
        Put("api/v1/school");
        // PreProcessor<AuthInterceptor<UpsertSchooldRequest>>();
        AllowAnonymous();
        Description(c => c.Accepts<UpsertSchoolResponse>()
            .Produces<UpsertSchoolResponse>()
            .ProducesProblem(400)
            .WithTags("School")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(UpsertSchoolRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}