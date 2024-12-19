
using Microsoft.AspNetCore.Mvc;
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Questions.FindAllFlows;

public record FindAllFlowsRequest(
    [FromRoute] string BuildQuestionId
) : AuthRequest;

