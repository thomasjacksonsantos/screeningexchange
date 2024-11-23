
using ScreeningExchange.Domain.Aggregates.ValueObjects;

namespace ScreeningExchange.Domain.Aggregates.StudentiesAggregate;

public class Student
{
    public Ulid Id { get; private set; }
    public Name Name { get; private set; }
    public Email Email { get; private set; }
    public Phone Phone { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? UpdatedOn { get; private set; }

#pragma warning disable CS8618
    private Student() { }
#pragma warning restore CS8618

    private Student(
        Name name,
        Email email,
        Phone phone
    )
    {
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

    public static Student Create(
        Name name,
        Email email,
        Phone phone
    )
        => new(
            name,
            email,
            phone
        );
}