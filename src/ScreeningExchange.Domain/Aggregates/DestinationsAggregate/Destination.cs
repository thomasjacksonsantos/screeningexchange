
using ScreeningExchange.Domain.Aggregates.QuestionsAggregate;
using ScreeningExchange.Domain.Aggregates.StudentiesAggregate;

namespace ScreeningExchange.Domain.Aggregates.DestinationsAggregate;

public class Destination
{
    public Ulid Id { get; private set; }
    public Ulid StudentId { get; private set; }
    public Student Student { get; private set; }
    public Ulid BuildQuestionId { get; private set; }
    public BuildQuestion BuildQuestion { get; private set; }
    public string QuestionId { get; private set; }
    public string Awnser { get; private set; }
    public DateTime DateTimeFinished { get; private set; }
    public DateTime CreatedOn { get; private set; }
    public DateTime? UpdatedOn { get; private set; }

#pragma warning disable CS8618
    private Destination() { }
#pragma warning restore CS8618

    private Destination(
        Student student,
        BuildQuestion buildQuestion,
        string questionId,
        string awnser
    )
    {
        if (student is null)
            throw new ArgumentNullException(nameof(student));

        if (buildQuestion is null)
            throw new ArgumentNullException(nameof(buildQuestion));

        Id = Ulid.NewUlid();
        StudentId = student.Id;
        Student = student;
        BuildQuestionId = buildQuestion.Id;
        BuildQuestion = buildQuestion;
        QuestionId = questionId;
        Awnser = awnser;
        CreatedOn = DateTime.Now;
        DateTimeFinished = DateTime.Now;
    }

    public void Update(
        string questionId,
        string awnser
    )
    {
        QuestionId = questionId;
        Awnser = awnser;
        DateTimeFinished = DateTime.Now;
        UpdatedOn = DateTime.Now;
    }

    public static Destination Create(
        Student student,
        BuildQuestion buildQuestion,
        string questionId,
        string awnser
    )
        => new(
            student,
            buildQuestion,
            questionId,
            awnser
        );
}