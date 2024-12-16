namespace ScreeningExchange.App.Api.Features.Schools.FindSchoolById;

public record class FindSchoolByIdResponse
(
    string Id,
    string Name,
    string Email,
    string Phone
);