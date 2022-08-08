using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Shared.Tests.Notes;

public class NoteIdBuilder
{
    private readonly Guid id;

    public NoteIdBuilder(Guid Id)
    {
        id = Id;
    }
    
    public static implicit operator Guid(NoteIdBuilder builder) => builder.id;
    public static implicit operator NoteId(NoteIdBuilder builder) => new(builder.id);
}