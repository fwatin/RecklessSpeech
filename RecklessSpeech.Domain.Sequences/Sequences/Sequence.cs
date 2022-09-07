using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Sequences;

public sealed class Sequence
{
    public SequenceId SequenceId{ get; private init; }
    public HtmlContent HtmlContent { get; private init; }
    public Word Word { get; private init; }
    private TranslatedSentence translatedSentence = default!;
    private AudioFileNameWithExtension audioFile= default!;
    private Tags tags= default!;

    private Sequence(SequenceId sequenceId)
    {
        this.SequenceId = sequenceId;
    }

    public IEnumerable<IDomainEvent> Import()
    {
        yield return new AddedSequenceEvent(
            this.SequenceId,
            this.HtmlContent,
            this.audioFile,
            this.tags,
            this.Word,
            this.translatedSentence);
    }

    public static Sequence Create(
        Guid id,
        HtmlContent htmlContent, 
        AudioFileNameWithExtension audioFileNameWithExtension, 
        Tags tags,
        Word word,
        TranslatedSentence translatedSentence)
    {
        return new Sequence(new(id))
        {
            HtmlContent = htmlContent,
            audioFile = audioFileNameWithExtension,
            tags = tags,
            Word = word,
            translatedSentence = translatedSentence
        };
    }
}