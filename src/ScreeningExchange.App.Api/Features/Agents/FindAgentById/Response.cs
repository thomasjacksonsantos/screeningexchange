using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Agents.FindAgentById;

public record class FindAgentByIdResponse
(
    string Id,
    string Name,
    string AgentEmail,
    string Phone
) : AuthRequest;