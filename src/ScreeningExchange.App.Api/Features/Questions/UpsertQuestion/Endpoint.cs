using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.App.Api.Features.Shared.Auth;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Questions.UpsertQuestion;

public class Endpoint(IInputOutputPortUseCase<UpsertQuestiondRequest, IUseCaseOutputPort<Result<UpsertQuestionResponse>>, Result<UpsertQuestionResponse>> useCase)
    : Endpoint<UpsertQuestiondRequest, Result<UpsertQuestionResponse>>
{
    private readonly IInputOutputPortUseCase<UpsertQuestiondRequest, IUseCaseOutputPort<Result<UpsertQuestionResponse>>, Result<UpsertQuestionResponse>> useCase = useCase;

    public override void Configure()
    {
        Put("api/v1/question");
        PreProcessor<AuthInterceptor<UpsertQuestiondRequest>>();
        AllowAnonymous();
        Description(c => c.Accepts<UpsertQuestiondRequest>()
            .Produces<UpsertQuestionResponse>()
            .ProducesProblem(400)
            .WithTags("Questions")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(UpsertQuestiondRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}