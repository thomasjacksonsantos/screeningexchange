
namespace ScreeningExchange.Domain.Aggregates.PerguntasAggregate;

public class GeradorPergunta
{
    public Dictionary<Ulid, Pergunta> Perguntas { get; private set; }
    public Dictionary<(Ulid, string), Ulid> Fluxo { get; set; }
    private Ulid perguntaAtualId;

    private GeradorPergunta()
    {
        Perguntas ??= new();
        Fluxo ??= new();
    }

    public void AddPergunta(
        Ulid id,
        Pergunta pergunta
    )
        => Perguntas[id] = pergunta;

    public void DefinirFluxo(
        Ulid perguntaId,
        string resposta,
        Ulid proximaPerguntaId
    )
        => Fluxo[(perguntaId, resposta)] = proximaPerguntaId;


    public void IniciarPergunta(
        Ulid perguntaInicialId
    )
        => perguntaAtualId = perguntaInicialId;

    public Pergunta? Responder(
        string resposta
    )
    {
        if (Fluxo.TryGetValue((perguntaAtualId, resposta), out Ulid proximaPerguntaId))
            perguntaAtualId = proximaPerguntaId;

        return ExibirPerguntaAtual();
    }

    public Pergunta? ExibirPerguntaAtual()
    {
        if (Perguntas.TryGetValue(perguntaAtualId, out Pergunta pergunta))
            return pergunta;

        return null;
    }

    public static GeradorPergunta Create()
        => new();
}