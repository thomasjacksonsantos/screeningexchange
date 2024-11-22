

namespace ScreeningExchange.Domain.Aggregates.ValueObjects;

public sealed class Name
{
    public string Value { get; private set; }

#pragma warning disable CS8618
    private Name() { }
#pragma warning restore CS8618

    private Name(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        Value = value.ToLower();
    }

    public static Name Create(string value)
        => new(value);
}