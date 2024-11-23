
using Microsoft.AspNetCore.Mvc;
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Questions.FindNextQuestionByAwnser;

public record FindNextQuestionByAwnserRequest(
    [FromRoute] string BuildQuestionId,
    [FromRoute] string QuestionId,
    [FromRoute] string Awnser
) : AuthRequest;

