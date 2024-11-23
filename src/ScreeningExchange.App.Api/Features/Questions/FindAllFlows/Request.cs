
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Questions.FindAllFlows;

public record FindAllFlowsRequest(
    string BuildQuestionId
) : AuthRequest;

