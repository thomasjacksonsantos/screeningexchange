
using ScreeningExchange.App.Api.Features.Shared.Auth;

namespace ScreeningExchange.App.Api.Features.Questions.FindAllQuestion;

public record FindAllQuestionRequest(
    string Id
) : AuthRequest;

