
namespace ScreeningExchange.Domain.Aggregates.QuestionsAggregate;

public class BuildQuestion
{
    public Ulid Id { get; private set; }
    public Dictionary<string, Question> Questions { get; private set; }
    public Dictionary<(string, string), string> Fluxo { get; set; }
    private string questionActualId;

#pragma warning disable CS8618
    private BuildQuestion()
    {
        Id = Ulid.NewUlid();
        Questions ??= new();
        Fluxo ??= new();
    }
#pragma warning restore CS8618

    public void CleanAll()
    {
        Questions = new();
        Fluxo = new();
    }

    public void AddQuestion(
        string id,
        Question question
    )
        => Questions[id] = question;

    public void DefinirFluxo(
        string questionId,
        string resposta,
        string nextQuestionId
    )
        => Fluxo[(questionId, resposta)] = nextQuestionId;


    public void IniciarQuestion(
        string questionInicialId
    )
        => questionActualId = questionInicialId;

    public Question? Responder(
        string resposta
    )
    {
        if (Fluxo.TryGetValue((questionActualId, resposta), out string? nextQuestionId))
            questionActualId = nextQuestionId;

        return ExibirQuestionAtual();
    }

    public Question? ExibirQuestionAtual()
    {
        if (Questions.TryGetValue(questionActualId, out Question? question))
            return question;

        return null;
    }

    public static BuildQuestion Create()
        => new();
}