

using ScreeningExchange.Domain.Aggregates.PerguntasAggregate;

namespace ScrenningExchange.Perguntas;

public class GeradorPerguntasTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        // Arrange
        var g = GeradorPergunta.Create();
        var perguntaId1 = Ulid.NewUlid();
        var perguntaId2 = Ulid.NewUlid();
        var perguntaId3 = Ulid.NewUlid();
        var perguntaId4 = Ulid.NewUlid();

        g.AddPergunta(perguntaId1, Pergunta.Create(
                "Qual país deseja fazer intercambio ?",
                new string[] { "Brasil", "Canada", "USA" }
            )
        );

        g.AddPergunta(perguntaId2, Pergunta.Create(
                "Qual Cidade ou Provincia",
                new string[] { "Sao Paulo", "Rio de Janeiro" }
            )
        );

        g.AddPergunta(perguntaId3, Pergunta.Create(
                "Qual Cidade ou Provincia",
                new string[] { "Toronto", "Vancouver" }
            )
        );

        g.AddPergunta(perguntaId4, Pergunta.Create(
                "Voce vai a trabalho ou estudo ?",
                new string[] { "Trabalho", "Estudo" }
            )
        );

        g.DefinirFluxo(perguntaId1, "Brasil", perguntaId2);
        g.DefinirFluxo(perguntaId1, "Canada", perguntaId3);
        g.DefinirFluxo(perguntaId2, "Sao Paulo", perguntaId4);

        g.IniciarPergunta(perguntaId1);

        var perguntaAtual = g.ExibirPerguntaAtual();

        Assert.That(perguntaAtual!.Texto, Is.EqualTo("Qual país deseja fazer intercambio ?"));

        g.IniciarPergunta(perguntaId4);
        var perguntaSaoPaulo = g.ExibirPerguntaAtual();

        Assert.That(perguntaSaoPaulo!.Texto, Is.EqualTo("Voce vai a trabalho ou estudo ?"));

    }
}