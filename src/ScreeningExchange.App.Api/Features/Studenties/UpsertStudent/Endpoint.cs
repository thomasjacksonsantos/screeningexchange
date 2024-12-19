using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Studenties.UpsertStudent;

public class Endpoint(IInputOutputPortUseCase<UpsertStudentRequest, IUseCaseOutputPort<Result<UpsertStudentResponse>>, Result<UpsertStudentResponse>> useCase)
    : Endpoint<UpsertStudentRequest, Result<UpsertStudentResponse>>
{
    private readonly IInputOutputPortUseCase<UpsertStudentRequest, IUseCaseOutputPort<Result<UpsertStudentResponse>>, Result<UpsertStudentResponse>> useCase = useCase;

    public override void Configure()
    {
        Put("api/v1/student");
        // PreProcessor<AuthInterceptor<UpsertStudentRequest>>();
        AllowAnonymous();
        Description(c => c.Accepts<UpsertStudentRequest>()
            .Produces<UpsertStudentResponse>()
            .ProducesProblem(400)
            .WithTags("Studenties")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(UpsertStudentRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}