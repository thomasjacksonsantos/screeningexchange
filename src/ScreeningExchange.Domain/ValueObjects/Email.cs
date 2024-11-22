using System.ComponentModel.DataAnnotations;

namespace ScreeningExchange.Domain.Aggregates.ValueObjects;

public sealed class Email
{
    public string Value { get; private set; }

#pragma warning disable CS8618
    private Email() { }
#pragma warning restore CS8618

    private Email(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentNullException(nameof(value));

        var mail = new EmailAddressAttribute();
        if (!mail.IsValid(value))
            throw new ArgumentException("Email invalid", value);

        Value = value;
    }

    public static Email Create(string value)
        => new(value);
}