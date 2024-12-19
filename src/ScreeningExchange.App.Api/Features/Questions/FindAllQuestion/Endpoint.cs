using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Questions.FindAllQuestion;

public class Endpoint(IInputOutputPortUseCase<FindAllQuestionRequest, IUseCaseOutputPort<Result<FindAllQuestionResponse>>, Result<FindAllQuestionResponse>> useCase)
    : Endpoint<FindAllQuestionRequest, Result<FindAllQuestionResponse>>
{
    private readonly IInputOutputPortUseCase<FindAllQuestionRequest, IUseCaseOutputPort<Result<FindAllQuestionResponse>>, Result<FindAllQuestionResponse>> useCase = useCase;

    public override void Configure()
    {
        Get("api/v1/question/all");
        // PreProcessor<AuthInterceptor<FindAllQuestionRequest>>();
        AllowAnonymous();
        Description(c => c.Accepts<FindAllQuestionRequest>()
            .Produces<FindAllQuestionResponse>()
            .ProducesProblem(400)
            .WithTags("Questions")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(FindAllQuestionRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}