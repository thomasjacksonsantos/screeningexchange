
namespace ScreeningExchange.App.Api.Features.Destinations.FindAllDestinationForSchool;

public record class FindAllDestinationForSchoolResponse
(
    IEnumerable<DestinationResponse>? Destinations
);

public record DestinationResponse
(
    StudentResponse Student,
    QuestionResponse Question
);

public record StudentResponse(
    string Id,
    string Name,
    string Email,
    string Phone
);

public record QuestionResponse(
    string Text
);