

namespace ScreeningExchange.App.Api.Features.Studenties.UpsertStudent;

public record UpsertStudentRequest(
    string? Id,
    string Name,
    string StudentEmail,
    string Phone

);