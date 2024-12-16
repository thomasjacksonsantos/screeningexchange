
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Studenties.UpsertStudent;

public record UpsertStudentdRequest(
    string? Id,
    string Name,
    string EmailStudent,
    string Phone

) : AuthRequest;