using RecklessSpeech.Application.Write.Sequences.Commands;
using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Shared.Tests.Notes;

public record NoteBuilder
{
    public NoteIdBuilder Id { get; init; }
    public QuestionBuilder Question { get; init; }

    private NoteBuilder(NoteIdBuilder id, QuestionBuilder question)
    {
        this.Id = id;
        this.Question = question;
    }

    public SendNotesCommand BuildCommand()
    {
        return new SendNotesCommand(new Guid[] {Id});
    }

    public Note BuildAggregate()
    {
        return Note.Hydrate(Id, Question);
    }

    public static NoteBuilder Create(Guid id)
    {
        return new NoteBuilder(new(id), new());
    }

    public NoteDto BuildDto()
    {
        return new NoteDto(this.Question);
    }
}