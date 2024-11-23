namespace ScreeningExchange.App.Api.Features.Questions.FindNextQuestionByAwnser;

public record class FindNextQuestionByAwnserResponse
(
    bool IsFinishQuestion,
    string? QuestionId = null,
    string? Text = null,
    ICollection<string>? Awnsers = null
);