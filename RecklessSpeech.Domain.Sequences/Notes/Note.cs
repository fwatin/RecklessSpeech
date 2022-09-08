using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Domain.Sequences.Notes;

public sealed class Note
{
    public NoteId Id { get; }
    private readonly Question question;
    private readonly After after;

    private Note(NoteId id, Question question, After after)
    {
        this.question = question;
        this.after = after;
        this.Id = id;
    }

    public static Note Hydrate(NoteId id, Question question, After after) => new(id, question, after);
    public static Note CreateFromSequence(Sequence sequence)
    {
        string after = sequence.Explanation is null
            ? ""
            : sequence.Explanation.Content.Value;
        
        return new Note(
            new(Guid.NewGuid()),
            Question.Create(sequence!.HtmlContent),
            After.Create(after)
        );
    }

    public NoteDto GetDto()
    {
        return new NoteDto(this.question, this.after);
    }
}