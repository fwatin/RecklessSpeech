using RecklessSpeech.Application.Write.Sequences.Commands.Notes.Send;
using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Shared.Tests.Notes
{
    public record NoteBuilder
    {
        private NoteBuilder(NoteIdBuilder id, QuestionBuilder question, AnswerBuilder answer, AfterBuilder after,
            AudioBuilder audio)
        {
            this.Id = id;
            this.Question = question;
            this.After = after;
            this.Audio = audio;
        }

        public NoteIdBuilder Id { get; init; }
        public QuestionBuilder Question { get; init; }
        public AnswerBuilder Answer { get; init; }
        public AfterBuilder After { get; init; }
        public SourceBuilder Source { get; init; }
        public AudioBuilder Audio { get; init; }

        public SendNotesCommand BuildCommand() =>
            new(new Guid[] { this.Id });

        public Note BuildAggregate() =>
            Note.Hydrate(this.Id, this.Question, this.Answer, this.After, this.Source, this.Audio);

        public static NoteBuilder Create(Guid id) =>
            new(
                new(id),
                new(),
                new(),
                new(),
                new());

        public NoteDto BuildDto() => new(this.Question, this.Answer, this.After, this.Source, this.Audio);
    }
}