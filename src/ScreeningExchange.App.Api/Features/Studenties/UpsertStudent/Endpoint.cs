using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.App.Api.Features.Shared.Auth;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Studenties.UpsertStudent;

public class Endpoint(IInputOutputPortUseCase<UpsertStudentdRequest, IUseCaseOutputPort<Result<UpsertStudentResponse>>, Result<UpsertStudentResponse>> useCase)
    : Endpoint<UpsertStudentdRequest, Result<UpsertStudentResponse>>
{
    private readonly IInputOutputPortUseCase<UpsertStudentdRequest, IUseCaseOutputPort<Result<UpsertStudentResponse>>, Result<UpsertStudentResponse>> useCase = useCase;

    public override void Configure()
    {
        Put("api/v1/student");
        // PreProcessor<AuthInterceptor<UpsertStudentdRequest>>();
        AllowAnonymous();
        Description(c => c.Accepts<UpsertStudentResponse>()
            .Produces<UpsertStudentResponse>()
            .ProducesProblem(400)
            .WithTags("Questions")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(UpsertStudentdRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}