

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

        g.AddPergunta("1", Pergunta.Create(
                "Qual país deseja fazer intercambio ?",
                new string[] { "Brasil", "Canada", "USA" }
            )
        );

        g.AddPergunta("2", Pergunta.Create(
                "Qual Cidade ou Provincia",
                new string[] { "Sao Paulo", "Rio de Janeiro" }
            )
        );

        g.AddPergunta("3", Pergunta.Create(
                "Qual Cidade ou Provincia",
                new string[] { "Toronto", "Vancouver" }
            )
        );

        g.AddPergunta("4", Pergunta.Create(
                "Voce vai a trabalho ou estudo ?",
                new string[] { "Trabalho", "Estudo" }
            )
        );

        g.DefinirFluxo("1", "Brasil", "2");
        g.DefinirFluxo("1", "Canada", "3");
        g.DefinirFluxo("2", "Sao Paulo", "4");

        g.IniciarPergunta("1");

        var perguntaAtual = g.ExibirPerguntaAtual();

        Assert.Pass();
    }
}