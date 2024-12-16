
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Schools.UpsertSchool;

public record UpsertSchoolRequest(
    string? Id,
    string Name,
    string EmailStudent,
    string Phone

) : AuthRequest;