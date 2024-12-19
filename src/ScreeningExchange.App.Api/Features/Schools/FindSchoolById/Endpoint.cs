using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Schools.FindSchoolById;

public class Endpoint(IInputOutputPortUseCase<FindSchoolByIdRequest, IUseCaseOutputPort<Result<FindSchoolByIdResponse>>, Result<FindSchoolByIdResponse>> useCase)
    : Endpoint<FindSchoolByIdRequest, Result<FindSchoolByIdResponse>>
{
    private readonly IInputOutputPortUseCase<FindSchoolByIdRequest, IUseCaseOutputPort<Result<FindSchoolByIdResponse>>, Result<FindSchoolByIdResponse>> useCase = useCase;

    public override void Configure()
    {
        Get("api/v1/school/find/{id}");
        AllowAnonymous();
        Description(c => c.Accepts<FindSchoolByIdRequest>()
            .Produces<FindSchoolByIdResponse>()
            .ProducesProblem(400)
            .WithTags("Schools")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(FindSchoolByIdRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}