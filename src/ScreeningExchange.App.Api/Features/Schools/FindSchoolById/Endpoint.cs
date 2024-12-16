using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.App.Api.Features.Shared.Auth;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Schools.FindSchoolById;

public class Endpoint(IInputOutputPortUseCase<FindSchoolByIdRequest, IUseCaseOutputPort<Result<FindSchoolByIdResponse>>, Result<FindSchoolByIdResponse>> useCase)
    : Endpoint<FindSchoolByIdRequest, Result<FindSchoolByIdResponse>>
{
    private readonly IInputOutputPortUseCase<FindSchoolByIdRequest, IUseCaseOutputPort<Result<FindSchoolByIdResponse>>, Result<FindSchoolByIdResponse>> useCase = useCase;

    public override void Configure()
    {
        Put("api/v1/school/find/{id}");
        // PreProcessor<AuthInterceptor<FindSchoolByIddRequest>>();
        AllowAnonymous();
        Description(c => c.Accepts<FindSchoolByIdResponse>()
            .Produces<FindSchoolByIdResponse>()
            .ProducesProblem(400)
            .WithTags("School")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(FindSchoolByIdRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}