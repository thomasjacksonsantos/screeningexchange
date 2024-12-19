
using Microsoft.AspNetCore.Mvc;
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Destinations.FindDestinationByStudentId;

public record FindDestinationByStudentIdRequest(
   [FromRoute] string Id

) : AuthRequest;