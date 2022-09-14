using System.Text;
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
        return new Note(
            new(Guid.NewGuid()),
            Question.Create(sequence!.HtmlContent),
            CreateAfter(sequence)
        );
    }

    private static After CreateAfter(Sequence sequence)
    {
        StringBuilder stringBuilder = new();
        
        stringBuilder.Append($"translated sentence from Netflix: \"{sequence.TranslatedSentence.Value}\"");

        if (sequence.Explanation is not null)
        {
            stringBuilder.AppendLine(sequence.Explanation.Content.Value);
        }
        
        return After.Create(stringBuilder.ToString());
    }

    public NoteDto GetDto()
    {
        return new NoteDto(this.question, this.after);
    }
}