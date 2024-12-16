
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Schools.FindSchoolById;

public record FindSchoolByIdRequest(
    string Id

) : AuthRequest;