
using Microsoft.AspNetCore.Mvc;
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Questions.FindQuestionById;

public record FindQuestionByIdRequest(
    [FromRoute] string BuildQuestionId,
    [FromRoute] string QuestionId
) : AuthRequest;

