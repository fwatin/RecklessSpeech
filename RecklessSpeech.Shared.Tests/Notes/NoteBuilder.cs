using RecklessSpeech.Application.Write.Sequences.Commands;
using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Shared.Tests.Notes;

public record NoteBuilder
{
    public NoteIdBuilder IdBuilder { get; init; }
    public QuestionBuilder QuestionBuilder { get; init; }

    private NoteBuilder(NoteIdBuilder idBuilder, QuestionBuilder questionBuilder)
    {
        this.IdBuilder = idBuilder;
        this.QuestionBuilder = questionBuilder;
    }

    public SendNotesCommand BuildCommand()
    {
        return new SendNotesCommand(new Guid[] {IdBuilder});
    }

    public Note BuildAggregate()
    {
        return Note.Hydrate(IdBuilder, QuestionBuilder);
    }

    public static NoteBuilder Create(Guid id)
    {
        return new NoteBuilder(new(id), new());
    }

    public NoteDto BuildDto()
    {
        return new NoteDto(this.QuestionBuilder);
    }
}