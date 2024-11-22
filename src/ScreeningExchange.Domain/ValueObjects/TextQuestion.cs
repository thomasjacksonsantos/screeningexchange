

namespace ScreeningExchange.Domain.Aggregates.ValueObjects;

public sealed class TextQuestion
{
    public string Value { get; private set; }

#pragma warning disable CS8618
    private TextQuestion() { }
#pragma warning restore CS8618

    private TextQuestion(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        Value = value.ToLower();
    }

    public static TextQuestion Create(string value)
        => new(value);
}