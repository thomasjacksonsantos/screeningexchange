

using ScreeningExchange.Domain.Aggregates.QuestionsAggregate;
using ScreeningExchange.Domain.Aggregates.ValueObjects;

namespace ScrenningExchange.Questions;

public class GeradorQuestionsTest
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Test1()
    {
        // Arrange
        var g = BuildQuestion.Create();
        var questionId1 = "1";
        var questionId2 = "2";
        var questionId3 = "3";
        var questionId4 = "4";

        g.AddQuestion(questionId1, Question.Create(
                TextQuestion.Create("Qual país deseja fazer intercambio ?"),
                new string[] { "Brasil", "Canada", "USA" }
            )
        );

        g.AddQuestion(questionId2, Question.Create(
                TextQuestion.Create("Qual Cidade ou Provincia"),
                new string[] { "Sao Paulo", "Rio de Janeiro" }
            )
        );

        g.AddQuestion(questionId3, Question.Create(
                TextQuestion.Create("Qual Cidade ou Provincia"),
                new string[] { "Toronto", "Vancouver" }
            )
        );

        g.AddQuestion(questionId4, Question.Create(
                TextQuestion.Create("Voce vai a trabalho ou estudo ?"),
                new string[] { "Trabalho", "Estudo" }
            )
        );

        g.DefinirFluxo(questionId1, "Brasil", questionId2);
        g.DefinirFluxo(questionId1, "Canada", questionId3);
        g.DefinirFluxo(questionId2, "Sao Paulo", questionId4);

        g.IniciarQuestion(questionId1);

        var questionAtual = g.ExibirQuestionAtual();

        Assert.That(questionAtual!.Text.Value, Is.EqualTo("qual país deseja fazer intercambio ?"));

        g.IniciarQuestion(questionId4);
        var questionSaoPaulo = g.ExibirQuestionAtual();

        Assert.That(questionSaoPaulo!.Text.Value, Is.EqualTo("voce vai a trabalho ou estudo ?"));

    }
}