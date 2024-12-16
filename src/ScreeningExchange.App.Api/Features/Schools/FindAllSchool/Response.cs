namespace ScreeningExchange.App.Api.Features.Schools.FindAllSchool;

public record class FindAllSchoolResponse
(
    IEnumerable<SchoolResponse>? Schools
);

public record SchoolResponse(
    string Id,
    string Name,
    string Email,
    string Phone
);