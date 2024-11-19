
namespace ScreeningExchange.Domain.Aggregates.PerguntasAggregate;

public class GeradorPergunta
{
    private readonly Dictionary<string, Pergunta> perguntas = new();
    private readonly Dictionary<(string, string), string> fluxo = new();
    private string perguntaAtualId;
    
    private GeradorPergunta() { }

    public void AddPergunta(
        string id,
        Pergunta pergunta
    )
        => perguntas[id] = pergunta;

    public void DefinirFluxo(
        string perguntaId,
        string resposta,
        string proximaPerguntaId
    )
        => fluxo[(perguntaId, resposta)] = proximaPerguntaId;


    public void IniciarPergunta(
        string perguntaInicialId
    )
        => perguntaAtualId = perguntaInicialId;

    public Pergunta? Responder(
        string resposta
    )
    {
        if (fluxo.TryGetValue((perguntaAtualId, resposta), out string proximaPerguntaId))
            perguntaAtualId = proximaPerguntaId;

        return ExibirPerguntaAtual();
    }

    public Pergunta? ExibirPerguntaAtual()
    {
        if (perguntas.TryGetValue(perguntaAtualId, out Pergunta pergunta))
            return pergunta;

        return null;
    }

    public static GeradorPergunta Create()
        => new();
}