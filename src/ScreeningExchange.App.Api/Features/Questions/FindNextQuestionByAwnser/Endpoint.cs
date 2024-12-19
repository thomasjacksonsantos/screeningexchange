using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.App.Api.Features.Shared.Auth;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Questions.FindNextQuestionByAwnser;

public class Endpoint(IInputOutputPortUseCase<FindNextQuestionByAwnserRequest, IUseCaseOutputPort<Result<FindNextQuestionByAwnserResponse>>, Result<FindNextQuestionByAwnserResponse>> useCase)
    : Endpoint<FindNextQuestionByAwnserRequest, Result<FindNextQuestionByAwnserResponse>>
{
    private readonly IInputOutputPortUseCase<FindNextQuestionByAwnserRequest, IUseCaseOutputPort<Result<FindNextQuestionByAwnserResponse>>, Result<FindNextQuestionByAwnserResponse>> useCase = useCase;

    public override void Configure()
    {
        Get("api/v1/build-question/{buildquestionid}/question/{questionid}/awnser/{awnser}");
        // PreProcessor<AuthInterceptor<FindNextQuestionByAwnserRequest>>();
        AllowAnonymous();
        Description(c => c.Accepts<FindNextQuestionByAwnserRequest>()
            .Produces<FindNextQuestionByAwnserResponse>()
            .ProducesProblem(400)
            .WithTags("Questions")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(FindNextQuestionByAwnserRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}