
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Questions.UpsertQuestion;

public record UpsertQuestiondRequest(
    string? Id,
    ICollection<QuestionRequest> Questions,
    ICollection<FlowRequest> Flows

) : AuthRequest;

public record QuestionRequest(
    string Id,
    string Text,
    ICollection<string> Awnsers
);

public record FlowRequest(
    string questionId,
    string Awnser,
    string NextQuestionId
);

