using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public sealed class PhraseSequence : Sequence
    {
        private PhraseSequence(
            SequenceId sequenceId,
            HtmlContent htmlContent,
            AudioFileNameWithExtension audioFileNameWithExtension,
            Phrase phrase,
            OriginalSentences originalSentences,
            SentenceTranslations sentenceTranslations,
            Media media,
            List<Explanation> explanations,
            Language language
        ) : base(sequenceId)
        {
            this.HtmlContent = htmlContent;
            this.AudioFile = audioFileNameWithExtension;
            this.Phrase = phrase;
            this.OriginalSentences = originalSentences;
            this.SentenceTranslations = sentenceTranslations;
            this.Media = media;
            this.Explanations = explanations;
            this.SentToAnkiTimes = 0;
            this.Language = language;

            var explanationSentence =
                $"translated sentence from Netflix: \"{SentenceTranslations.GetMainSentenceTranslation()}\"";
            Explanation explanationFromTranslation = Explanation.Create
            (
                explanationSentence,
                null,
                new(""),
                this.Language
            );


            this.Explanations.Add(explanationFromTranslation);
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
            return new(
                new(id),
                htmlContent,
                audioFileNameWithExtension,
                Phrase.Create(originalSentences.GetCentralSentence()),
                originalSentences,
                sentenceTranslations,
                media,
                explanations,
                language);
        }

        public override string SentenceToAskChatGptExplanation =>
            $"Peux-tu expliquer le sens de la phrase suivante : \"{this.Phrase.Value}\" ?";
        public override string ContentToGuessInTargetedLanguage() => this.SentenceTranslations.GetMainSentenceTranslation();
        public override string ContentToGuessInNativeLanguage() => this.Phrase.Value;

        public override string Translation
        {
            set
            {
            }
        }
    }
}