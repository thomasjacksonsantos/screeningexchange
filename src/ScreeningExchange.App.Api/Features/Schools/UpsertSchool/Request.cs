
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Schools.UpsertSchool;

public record UpsertSchoolRequest(
    string? Id,
    string SchoolName,
    string SchoolEmail,
    string Phone
) : AuthRequest;