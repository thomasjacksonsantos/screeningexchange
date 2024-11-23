
using ScreeningExchange.Domain.Aggregates.ValueObjects;

namespace ScreeningExchange.Domain.Aggregates.QuestionsAggregate;

public class Question
{
    public TextQuestion Text { get; private set; }
    public ICollection<string> Awnsers { get; private set; }

#pragma warning disable CS8618
    private Question() { }
#pragma warning restore CS8618

    private Question(
        TextQuestion text,
        ICollection<string> respostas
    )
    {
        if (respostas.Count == 0)
            throw new ArgumentNullException(nameof(respostas));

        Text = text;
        Awnsers = respostas;
    }

    public static Question Create(
        TextQuestion text,
        ICollection<string> awnsers
    )
        => new(
            text,
            awnsers
        );
}