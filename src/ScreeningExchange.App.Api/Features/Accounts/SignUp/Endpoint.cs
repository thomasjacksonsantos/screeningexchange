using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Accounts.SignUp;

public class Endpoint(IInputOutputPortUseCase<SignUpRequest, IUseCaseOutputPort<Result<SignUpResponse>>, Result<SignUpResponse>> useCase)
    : Endpoint<SignUpRequest, Result<SignUpResponse>>
{
    private readonly IInputOutputPortUseCase<SignUpRequest, IUseCaseOutputPort<Result<SignUpResponse>>, Result<SignUpResponse>> useCase = useCase;

    public override void Configure()
    {
        Post("api/v1/account/singup");
        AllowAnonymous();
        Description(c => c.Accepts<SignUpRequest>()
            .Produces<SignUpResponse>()
            .ProducesProblem(400)
            .WithTags("Accounts")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(SignUpRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}