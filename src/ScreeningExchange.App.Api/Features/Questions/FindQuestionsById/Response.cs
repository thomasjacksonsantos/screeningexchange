namespace ScreeningExchange.App.Api.Features.Questions.FindQuestionById;

public record class FindQuestionByIdResponse
(
    bool IsFinishQuestion,
    string? Text,
    ICollection<string>? Awnsers
);