

using ScreeningExchange.Domain.Enums;

namespace ScreeningExchange.Domain.Aggregates.LinkDispatchersAggregate;

public class Log
{
    public string Description { get; private set; }
    public LogStatusEnum Status { get; private set; }
    public DateTime CreatedOn { get; private set; }

#pragma warning disable CS8618
    private Log() { }
#pragma warning restore CS8618

    public Log(
        string description,
        LogStatusEnum status
    )
    {
        Description = description;
        Status = status;
        CreatedOn = DateTime.Now;
    }

    public static Log Create(
        string description,
        LogStatusEnum status
    )
        => new(
            description,
            status
        );
}