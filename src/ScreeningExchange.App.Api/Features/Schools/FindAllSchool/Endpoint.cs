using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.App.Api.Features.Shared.Auth;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Schools.FindAllSchool;

public class Endpoint(IInputOutputPortUseCase<FindAllSchoolRequest, IUseCaseOutputPort<Result<FindAllSchoolResponse>>, Result<FindAllSchoolResponse>> useCase)
    : Endpoint<FindAllSchoolRequest, Result<FindAllSchoolResponse>>
{
    private readonly IInputOutputPortUseCase<FindAllSchoolRequest, IUseCaseOutputPort<Result<FindAllSchoolResponse>>, Result<FindAllSchoolResponse>> useCase = useCase;

    public override void Configure()
    {
        Put("api/v1/school/all");
        // PreProcessor<AuthInterceptor<FindAllSchooldRequest>>();
        AllowAnonymous();
        Description(c => c.Accepts<FindAllSchoolResponse>()
            .Produces<FindAllSchoolResponse>()
            .ProducesProblem(400)
            .WithTags("School")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(FindAllSchoolRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}