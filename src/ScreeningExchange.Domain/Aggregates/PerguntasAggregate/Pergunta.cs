namespace ScreeningExchange.Domain.Aggregates.PerguntasAggregate;

public class Pergunta
{
    
    public string Texto { get; private set; }
    public ICollection<string> Respostas { get; private set; }
    public DateTime CreatedOn { get; private set; }

    private Pergunta(
        string texto,
        ICollection<string> respostas
    )
    {
        if (string.IsNullOrWhiteSpace(texto))
            throw new ArgumentNullException(nameof(texto));

        if (respostas.Count == 0)
            throw new ArgumentNullException(nameof(respostas));

        Texto = texto;
        Respostas = respostas;
        CreatedOn = DateTime.Now;
    }

    public static Pergunta Create(
        string texto,
        ICollection<string> respostas
    )
        => new(
            texto,
            respostas
        );
}