
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Destinations.FindAllDestinationForSchool;

public record FindAllDestinationForSchoolIdRequest(
    int Page,
    int Total

) : AuthRequest;