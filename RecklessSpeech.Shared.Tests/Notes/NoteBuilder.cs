using RecklessSpeech.Application.Write.Sequences.Commands;
using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Shared.Tests.Notes;

public record NoteBuilder
{
    public NoteIdBuilder Id { get; init; }
    public QuestionBuilder Question { get; init; }
    public AfterBuilder After { get; init; }
    public SourceBuilder Source { get; init; }

    private NoteBuilder(NoteIdBuilder id, QuestionBuilder question, AfterBuilder after)
    {
        this.Id = id;
        this.Question = question;
        this.After = after;
    }

    public SendNotesCommand BuildCommand()
    {
        return new SendNotesCommand(new Guid[]
        {
            this.Id
        });
    }

    public Note BuildAggregate()
    {
        return Note.Hydrate(this.Id, this.Question, this.After, this.Source);
    }

    public static NoteBuilder Create(Guid id)
    {
        return new NoteBuilder(
            new(id),
            new(),
            new());
    }

    public NoteDto BuildDto()
    {
        return new NoteDto(this.Question, this.After, this.Source);
    }
}