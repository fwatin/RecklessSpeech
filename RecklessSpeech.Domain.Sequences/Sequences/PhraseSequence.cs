using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public sealed class PhraseSequence : Sequence
    {
        private PhraseSequence(SequenceId sequenceId) : base(sequenceId)
        {
        }

        public Phrase Phrase { get; private init; } = default!;

        public static PhraseSequence Create(Guid id,
            HtmlContent htmlContent,
            AudioFileNameWithExtension audioFileNameWithExtension,
            OriginalSentences originalSentences,
            SentenceTranslations sentenceTranslations,
            Media media,
            List<Explanation> explanations,
            Language language)
        {
            return new(new(id))
            {
                Phrase = Phrase.Create(originalSentences.GetCentralSentence()),
                HtmlContent = htmlContent,
                AudioFile = audioFileNameWithExtension,
                OriginalSentences = originalSentences,
                SentenceTranslations = sentenceTranslations,
                Media = media,
                Explanations = explanations,
                SentToAnkiTimes = 0,
                Language = language
            };
        }

        public override string? ContentToGuessInTargetedLanguage() => this.SentenceTranslations.GetMainSentenceTranslation();
        public override string ContentToGuessInNativeLanguage() => this.Phrase.Value;

        public override string Translation
        {
            set
            {
            }
        }
    }
}