
using ScreeningExchange.Domain.Aggregates.ValueObjects;

namespace ScreeningExchange.Domain.Aggregates.AgentsAggregate;

public class Agent
{
    public Ulid Id { get; private set; }
    public string UserId { get; private set; }
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Phone Phone { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? UpdatedOn { get; private set; }

#pragma warning disable CS8618
    private Agent() { }
#pragma warning restore CS8618

    private Agent(
        string userId,
        Name name,
        Email email,
        Phone phone
    )
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentNullException(nameof(userId));

        Id = Ulid.NewUlid();
        UserId = userId;
        Name = name;
        Email = email;
        Phone = phone;
        CreatedOn = DateTime.Now;
    }

    public void Update(
        Name name,
        Email email,
        Phone phone
    )
    {
        Name = name;
        Email = email;
        Phone = phone;
        UpdatedOn = DateTime.Now;
    }

    public static Agent Create(
        string userId,
        Name name,
        Email email,
        Phone phone
    )
        => new(
            userId,
            name,
            email,
            phone
        );
}