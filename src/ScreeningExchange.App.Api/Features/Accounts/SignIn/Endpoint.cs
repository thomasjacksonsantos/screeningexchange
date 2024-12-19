using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Accounts.SignIn;

public class Endpoint(IInputOutputPortUseCase<SignInRequest, IUseCaseOutputPort<Result<SignInResponse>>, Result<SignInResponse>> useCase)
    : Endpoint<SignInRequest, Result<SignInResponse>>
{
    private readonly IInputOutputPortUseCase<SignInRequest, IUseCaseOutputPort<Result<SignInResponse>>, Result<SignInResponse>> useCase = useCase;

    public override void Configure()
    {
        Post("api/v1/account/singin");
        AllowAnonymous();
        Description(c => c.Accepts<SignInRequest>()
            .Produces<SignInResponse>()
            .ProducesProblem(400)
            .WithTags("Accounts")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(SignInRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}