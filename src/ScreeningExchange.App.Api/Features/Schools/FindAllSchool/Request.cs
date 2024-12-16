
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Schools.FindAllSchool;

public record FindAllSchoolRequest(
    int Page,
    int Total

) : AuthRequest;