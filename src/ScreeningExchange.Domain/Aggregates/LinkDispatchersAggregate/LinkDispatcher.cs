
using ScreeningExchange.Domain.Aggregates.QuestionsAggregate;
using ScreeningExchange.Domain.Enums;

namespace ScreeningExchange.Domain.Aggregates.LinkDispatchersAggregate;

public class LinkDispatcher
{
    public Ulid Id { get; private set; }
    public Ulid BuildQuestionId { get; private set; }
    public BuildQuestion BuildQuestion { get; private set; }
    public Customer Customer { get; private set; }
    public string Link { get; private set; }
    public bool SendToEmail { get; private set; }
    public bool SendToWhatsApp { get; private set; }
    public bool EmailSentSuccess { get; private set; }
    public bool WhatsappSentSuccess { get; private set; }
    public bool WasRead { get; private set; }
    public LinkDispatcherStatusEnum Status { get; private set; }
    public ICollection<Log> Logs { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? UpdatedOn { get; private set; }

#pragma warning disable CS8618
    private LinkDispatcher() { }
#pragma warning restore CS8618

    private LinkDispatcher(
        BuildQuestion buildQuestion,
        Customer customer,
        string link
    )
    {
        // if (id == Ulid.Empty)
        //     throw new ArgumentNullException(nameof(id));

        Id = Ulid.NewUlid();
        BuildQuestion = buildQuestion;
        BuildQuestionId = buildQuestion.Id;
        Customer = customer;
        Link = link;
        Logs = new List<Log>();
        CreatedOn = DateTime.Now;        
    }

    public void EnableSendToEmail()
        => SendToEmail = true;

    public void EnableSendToWhatsapp()
        => SendToWhatsApp = true;

    public void WasEmailSentSuccess()
        => EmailSentSuccess = true;

    public void ErroSendingEmail()
        => EmailSentSuccess = false;

    public void WasWhatsappSentSuccess()
        => WhatsappSentSuccess = true;

    public void ErroSendingWhatsapp()
        => WhatsappSentSuccess = false;

    public void CreateLog(Log log)
    {
        Logs ??= new List<Log>();
        Logs.Add(log);
    }

    public static LinkDispatcher Create(
        // Ulid id,
        BuildQuestion buildQuestion,
        Customer customer,
        string link
    )
        => new(
            // id,
            buildQuestion,
            customer,
            link
        );
}