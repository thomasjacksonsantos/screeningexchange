namespace ScreeningExchange.App.Api.Features.Questions.FindAllQuestion;

public record FindAllQuestionResponse(
    ICollection<AllQuestionResponse> BuildQuestions
);

public record AllQuestionResponse(
    string Id,
    ICollection<QuestionResponse> Questions,
    ICollection<FlowResponse> Flows

);

public record QuestionResponse(
    string Id,
    string Text,
    ICollection<string> Awnsers
);

public record FlowResponse(
    string questionId,
    string Awnser,
    string NextQuestionId
);