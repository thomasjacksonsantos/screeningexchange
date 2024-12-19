
using Microsoft.AspNetCore.Mvc;
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Schools.FindSchoolById;

public record FindSchoolByIdRequest(
   [FromRoute] string Id

) : AuthRequest;