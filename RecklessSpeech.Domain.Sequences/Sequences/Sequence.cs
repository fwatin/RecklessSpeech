using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Sequences;

public sealed class Sequence
{
    public SequenceId SequenceId { get; private init; }
    public LanguageDictionaryId LanguageDictionaryId { get; set; }
    public HtmlContent HtmlContent { get; private init; } = default!;
    public Word Word { get; private init; } = default!;
    public TranslatedSentence TranslatedSentence { get; private init; } = default!;

    public AudioFileNameWithExtension AudioFile = default!;

    private Tags tags = default!;
    public Explanation? Explanation { get; init; } = default!;
    public TranslatedWord? TranslatedWord { get; init; } = default!;

    private Sequence(SequenceId sequenceId)
    {
        this.SequenceId = sequenceId;
    }

    public IEnumerable<IDomainEvent> SetDetails()
    {
        yield return new SetTranslatedWordEvent(this.SequenceId, this.TranslatedWord!);
    }

    public IEnumerable<IDomainEvent> Import()
    {
        yield return new AddedSequenceEvent(
            this.SequenceId,
            this.HtmlContent,
            this.AudioFile,
            this.tags,
            this.Word,
            this.TranslatedSentence);
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
            AudioFile = audioFileNameWithExtension,
            tags = tags,
            Word = word,
            TranslatedSentence = translatedSentence
        };
    }

    public static Sequence Hydrate(
        Guid id,
        string htmlContent,
        string audioFileNameWithExtension,
        string tags,
        string word,
        string translatedSentence,
        Explanation? explanation, //todo devrait pas etre le type
        string? translatedWord)
    {
        return new Sequence(new(id))
        {
            HtmlContent = HtmlContent.Hydrate(htmlContent),
            AudioFile = AudioFileNameWithExtension.Hydrate(audioFileNameWithExtension),
            tags = Tags.Hydrate(tags),
            Word = Word.Hydrate(word),
            TranslatedSentence = TranslatedSentence.Hydrate(translatedSentence),
            Explanation = explanation,
            TranslatedWord = TranslatedWord.Hydrate(translatedWord)
        };
    }
    public IEnumerable<IDomainEvent> SetDictionary(Guid languageDictionaryId)
    {
        this.LanguageDictionaryId = new(languageDictionaryId);

        yield return new AssignLanguageDictionaryInASequenceEvent(
            this.SequenceId,
            this.LanguageDictionaryId
        );
    }
}