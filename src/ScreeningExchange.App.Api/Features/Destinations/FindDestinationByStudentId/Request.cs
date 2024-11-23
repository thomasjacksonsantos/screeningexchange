
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Destinations.FindDestinationByStudentId;

public record FindDestinationByStudentIddRequest(
    string Id

) : AuthRequest;