using RecklessSpeech.Application.Write.Sequences.Commands.Notes.Send;
using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Shared.Tests.Notes
{
    public record NoteBuilder
    {
        private NoteBuilder(
            NoteIdBuilder id,
            QuestionBuilder question,
            AnswerBuilder answer,
            AfterBuilder after,
            SourceBuilder source,
            AudioBuilder audio)
        {
            this.Id = id;
            this.Question = question;
            this.Answer = answer;
            this.After = after;
            this.Source = source;
            this.Audio = audio;
        }

        private NoteIdBuilder Id { get; }
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
                new(),
                new());

        public NoteDto BuildDto() => new(this.Question, this.Answer, this.After, this.Source, this.Audio);
    }
}