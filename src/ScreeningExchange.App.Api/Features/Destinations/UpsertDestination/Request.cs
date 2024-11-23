
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Destinations.UpsertDestination;

public record UpsertDestinationdRequest(
    string? Id,
    string StudentId,
    string BuildQuestionId,
    string QuestionId,
    string Awnser

) : AuthRequest;