using RecklessSpeech.Application.Core.Events;
using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public sealed class Sequence
    {
        public AudioFileNameWithExtension AudioFile = default!;

        private Tags tags = default!;

        private Sequence(SequenceId sequenceId) => this.SequenceId = sequenceId;

        public SequenceId SequenceId { get; }
        public HtmlContent HtmlContent { get; private init; } = default!;
        public Word Word { get; private init; } = default!;
        public TranslatedSentence TranslatedSentence { get; private init; } = default!;
        public Explanation? Explanation { get; set; }
        public TranslatedWord? TranslatedWord { get; set; }
        public MediaId MediaId { get;  private init; }= default!;


        public IEnumerable<IDomainEvent> Import()
        {
            yield return new ImportedSequenceEvent(
                this.SequenceId,
                this.HtmlContent,
                this.AudioFile,
                this.tags,
                this.Word,
                this.TranslatedSentence,
                this.TranslatedWord,
                this.MediaId);
        }

        public static Sequence Create(Guid id,
            HtmlContent htmlContent,
            AudioFileNameWithExtension audioFileNameWithExtension,
            Tags tags,
            Word word,
            TranslatedSentence translatedSentence,
            MediaId mediaId) =>
            new(new(id))
            {
                HtmlContent = htmlContent,
                AudioFile = audioFileNameWithExtension,
                tags = tags,
                Word = word,
                TranslatedSentence = translatedSentence,
                MediaId = mediaId
            };

        public static Sequence Hydrate(
            Guid id,
            string htmlContent,
            string audioFileNameWithExtension,
            string tags,
            string word,
            string translatedSentence,
            long mediaId,
            Explanation? explanation,
            string? translatedWord) =>
            new(new(id))
            {
                HtmlContent = HtmlContent.Hydrate(htmlContent),
                AudioFile = AudioFileNameWithExtension.Hydrate(audioFileNameWithExtension),
                tags = Tags.Hydrate(tags),
                Word = Word.Hydrate(word),
                TranslatedSentence = TranslatedSentence.Hydrate(translatedSentence),
                Explanation = explanation,
                TranslatedWord = TranslatedWord.Hydrate(translatedWord),
                MediaId = MediaId.Hydrate(mediaId)
            };
    }
}