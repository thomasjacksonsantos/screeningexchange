namespace ScreeningExchange.App.Api.Features.Questions.FindAllFlows;

public record FindAllFlowsResponse(
    ICollection<FlowResponse> Flows

);

public record FlowResponse(
    string questionId,
    string Awnser,
    string NextQuestionId
);