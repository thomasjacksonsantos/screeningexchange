
using ScreeningExchange.Domain.Aggregates.ValueObjects;

namespace ScreeningExchange.Domain.Aggregates.SchoolsAggregate;

public class School
{
    public Ulid Id { get; private set; }
    public string UserId { get; set; }
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Phone Phone { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? UpdatedOn { get; private set; }

#pragma warning disable CS8618
    private School() { }
#pragma warning restore CS8618

    private School(
        string userId,
        Name name,
        Email email,
        Phone phone
    )
    {
        if (string.IsNullOrWhiteSpace(userId))
            throw new ArgumentNullException(nameof(userId));

        UserId = userId;

        Id = Ulid.NewUlid();
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

    public static School Create(
        Name name,
        Email email,
        Phone phone,
        string userId
    )
        => new(
            userId,
            name,
            email,
            phone
        );
}