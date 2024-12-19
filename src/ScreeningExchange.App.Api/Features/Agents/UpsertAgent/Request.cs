
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Agents.UpsertAgent;

public record UpsertAgentRequest(
    string? Id,
    string Name,
    string AgentEmail,
    string Phone

) : AuthRequest;