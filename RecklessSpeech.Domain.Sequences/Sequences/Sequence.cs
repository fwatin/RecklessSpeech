using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Sequences;

public sealed class Sequence
{
    public SequenceId SequenceId { get; private init; }
    public HtmlContent HtmlContent { get; private init; } = default!;
    public Word Word { get; private init; } = default!;
    private TranslatedSentence translatedSentence = default!;
    private AudioFileNameWithExtension audioFile = default!;
    private Tags tags = default!;
    public Explanation? Explanation { get; init; } = default!;

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

    public static Sequence Hydrate(
        Guid id,
        string htmlContent,
        string audioFileNameWithExtension,
        string tags,
        string word,
        string translatedSentence,
        Explanation? explanation)
    {
        return new Sequence(new(id))
        {
            HtmlContent = HtmlContent.Hydrate(htmlContent),
            audioFile = AudioFileNameWithExtension.Hydrate(audioFileNameWithExtension),
            tags = Tags.Hydrate(tags),
            Word = Word.Hydrate(word),
            translatedSentence = TranslatedSentence.Hydrate(translatedSentence),
            Explanation = explanation
        };
    }
}