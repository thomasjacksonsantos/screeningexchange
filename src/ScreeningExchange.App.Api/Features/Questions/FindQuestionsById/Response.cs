namespace ScreeningExchange.App.Api.Features.Questions.FindQuestionsById;

public record class FindQuestionByIdResponse
(
    bool IsFinishQuestion,
    string? Text,
    ICollection<string>? Awnsers
);