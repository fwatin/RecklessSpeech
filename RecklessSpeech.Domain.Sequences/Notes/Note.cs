namespace RecklessSpeech.Domain.Sequences.Notes;

public sealed class Note
{
    public NoteId Id { get; }
    private readonly Question question;

    private Note(NoteId id, Question question)
    {
        this.question = question;
        this.Id = id;
    }

    public static Note Hydrate(NoteId id, Question question) => new(id, question);

    public NoteDto GetDto()
    {
        return new NoteDto(this.question);
    }
}

public record NoteDto(Question Question);