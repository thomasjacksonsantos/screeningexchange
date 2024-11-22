

namespace ScreeningExchange.Domain.Aggregates.ValueObjects;

public sealed class Phone
{
    public string Value { get; private set; }

#pragma warning disable CS8618
    private Phone() { }
#pragma warning restore CS8618

    private Phone(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        Value = value.ToLower();
    }

    public static Phone Create(string value)
        => new(value);
}