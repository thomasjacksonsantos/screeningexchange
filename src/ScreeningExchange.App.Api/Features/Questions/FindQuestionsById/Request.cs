
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Questions.FindQuestionsById;

public record FindQuestionByIdRequest(
    string Id,
    string QuestionId
) : AuthRequest;

