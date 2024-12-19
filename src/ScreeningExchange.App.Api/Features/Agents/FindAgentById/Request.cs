
using Microsoft.AspNetCore.Mvc;
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Agents.FindAgentById;

public record FindAgentByIdRequest(
    [FromRoute] string Id

) : AuthRequest;