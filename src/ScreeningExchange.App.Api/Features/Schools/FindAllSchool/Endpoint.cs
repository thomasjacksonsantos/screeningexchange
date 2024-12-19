using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Schools.FindAllSchool;

public class Endpoint(IInputOutputPortUseCase<FindAllSchoolRequest, IUseCaseOutputPort<Result<FindAllSchoolResponse>>, Result<FindAllSchoolResponse>> useCase)
    : Endpoint<FindAllSchoolRequest, Result<FindAllSchoolResponse>>
{
    private readonly IInputOutputPortUseCase<FindAllSchoolRequest, IUseCaseOutputPort<Result<FindAllSchoolResponse>>, Result<FindAllSchoolResponse>> useCase = useCase;

    public override void Configure()
    {
        Get("api/v1/school/all");
        Description(c => c.Accepts<FindAllSchoolRequest>()
            .Produces<FindAllSchoolResponse>()
            .ProducesProblem(400)
            .WithTags("Schools")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(FindAllSchoolRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}