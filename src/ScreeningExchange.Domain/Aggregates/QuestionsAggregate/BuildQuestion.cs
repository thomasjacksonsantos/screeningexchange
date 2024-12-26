
using System.ComponentModel.DataAnnotations.Schema;

namespace ScreeningExchange.Domain.Aggregates.QuestionsAggregate;

public class BuildQuestion
{
    public Ulid Id { get; private set; }
    public bool SendToEmail { get; private set; }
    public bool SendToWhatsApp { get; private set; }
    public List<QuestionCollection> Questions { get; private set; }
    public List<FlowCollection> Flows { get; private set; }
    [NotMapped]
    public string QuestionIdActual { get; private set; }

    public class QuestionCollection
    {
        public string Id { get; private set; }
        public Question Question { get; private set; }

#pragma warning disable CS8618
        private QuestionCollection() { }
#pragma warning restore CS8618

        private QuestionCollection(
            string id,
            Question question
        )
        {
            if (string.IsNullOrEmpty(id))
                throw new ArgumentNullException(nameof(id));

            Id = id;
            Question = question;
        }

        public static QuestionCollection Create(
            string id,
            Question question
        )
            => new(
                id,
                question
            );
    }

    public class FlowCollection
    {
        public string QuestionId { get; private set; }
        public string Awnser { get; private set; }
        public string NextQuestionId { get; private set; }

#pragma warning disable CS8618
        private FlowCollection() { }
#pragma warning restore CS8618

        private FlowCollection(
            string questionId,
            string awnser,
            string nextQuestionId
        )
        {
            if (string.IsNullOrEmpty(questionId))
                throw new ArgumentNullException(nameof(questionId));

            if (string.IsNullOrEmpty(awnser))
                throw new ArgumentNullException(nameof(awnser));

            QuestionId = questionId;
            Awnser = awnser.ToLower();
            NextQuestionId = nextQuestionId;
        }

        public static FlowCollection Create(
              string questionId,
            string awnser,
            string nextQuestionId
        )
            => new(
                questionId,
                awnser,
                nextQuestionId
            );
    }

#pragma warning disable CS8618
    private BuildQuestion()
    {
        Id = Ulid.NewUlid();
        Questions ??= new();
        Flows ??= new();
    }
#pragma warning restore CS8618

    public void CleanAll()
    {
        Questions = new();
        Flows = new();
    }

    public void AddQuestion(
        string id,
        Question question
    )
        => Questions.Add(QuestionCollection.Create(id, question));

    public void DefinirFluxo(
        string questionId,
        string awnser,
        string nextQuestionId
    )
        => Flows.Add(FlowCollection.Create(questionId, awnser, nextQuestionId));


    public void IniciarQuestion(
        string questionInicialId
    )
        => QuestionIdActual = questionInicialId;

    public Question? Awnser(
        string awnser
    )
    {
        var flow = Flows.FirstOrDefault(c => c.QuestionId == QuestionIdActual && c.Awnser == awnser.ToLower());

        if (flow is not null)
        {
            QuestionIdActual = flow.NextQuestionId;
        }

        return ExibirQuestionAtual();
    }

    public Question? ExibirQuestionAtual()
    {
        var question = Questions.FirstOrDefault(c => c.Id == QuestionIdActual);
        if (question is not null)
        {
            return question.Question;
        }

        return null;
    }

    public void EnableSendToEmail()
    {
        SendToEmail = true;
    }

    public void DisableSendToEmail()
    {
        SendToEmail = false;
    }

    public void EnableSendToWhatsApp()
    {
        SendToEmail = true;
    }

    public void DisableSendToWhatsApp()
    {
        SendToEmail = false;
    }

    public static BuildQuestion Create()
        => new();
}