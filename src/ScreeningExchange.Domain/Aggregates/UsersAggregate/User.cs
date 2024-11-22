
using ScreeningExchange.Domain.Aggregates.ValueObjects;

namespace ScreeningExchange.Domain.Aggregates.UsuarioAggregate;

public class User
{
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Phone Phone { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? UpdatedOn { get; private set; }

#pragma warning disable CS8618
    private User(
        Name name,
        Email email,
        Phone phone
    )
    {
        Name = name;
        Email = email;
        Phone = phone;
        CreatedOn = DateTime.Now;
    }
#pragma warning restore CS8618
}