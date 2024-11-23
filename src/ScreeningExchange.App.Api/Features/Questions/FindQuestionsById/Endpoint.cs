using FastEndpoints;
using Microsoft.AspNetCore.Http;
using ScreeningExchange.App.Api.Features.Shared.Auth;
using ScreeningExchange.Infrastructure.Core;

namespace ScreeningExchange.App.Api.Features.Questions.FindQuestionById;

public class Endpoint(IInputOutputPortUseCase<FindQuestionByIdRequest, IUseCaseOutputPort<Result<FindQuestionByIdResponse>>, Result<FindQuestionByIdResponse>> useCase)
    : Endpoint<FindQuestionByIdRequest, Result<FindQuestionByIdResponse>>
{
    private readonly IInputOutputPortUseCase<FindQuestionByIdRequest, IUseCaseOutputPort<Result<FindQuestionByIdResponse>>, Result<FindQuestionByIdResponse>> useCase = useCase;

    public override void Configure()
    {
        Get("api/v1/build-question/{buildquestionid}/question/{questionid}");
        // PreProcessor<AuthInterceptor<FindQuestionByIdRequest>>();
        AllowAnonymous();
        Description(c => c.Accepts<FindQuestionByIdResponse>()
            .Produces<FindQuestionByIdResponse>()
            .ProducesProblem(400)
            .WithTags("Questions")
            , clearDefaults: false);
    }

    public override async Task HandleAsync(FindQuestionByIdRequest request, CancellationToken ct)
    {
        var result = await useCase.Execute(request, ct);
        await this.SendAsync(result);
    }
}