using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public sealed class WordSequence : Sequence
    {
        private WordSequence(
            SequenceId sequenceId,
            HtmlContent htmlContent,
            AudioFileNameWithExtension audioFileNameWithExtension,
            Word word,
            TranslatedWord translatedWord,
            OriginalSentences originalSentences,
            SentenceTranslations sentenceTranslations,
            Media media,
            List<Explanation> explanations,
            Language language
        ) : base(sequenceId)
        {
            this.HtmlContent = htmlContent;
            this.AudioFile = audioFileNameWithExtension;
            this.Word = word;
            this.TranslatedWord = translatedWord;
            this.OriginalSentences = originalSentences;
            this.SentenceTranslations = sentenceTranslations;
            this.Media = media;
            this.Explanations = explanations;
            this.SentToAnkiTimes = 0;
            this.Language = language;
            //this.Explanations.Add(new Explanation());
        }

        public Word Word { get; private init; } = default!;

        public TranslatedWord? TranslatedWord { get; set; }


        public static WordSequence Create(Guid id,
            HtmlContent htmlContent,
            AudioFileNameWithExtension audioFileNameWithExtension,
            Word word,
            TranslatedWord translatedWord,
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
                    word,
                    translatedWord,
                    originalSentences,
                    sentenceTranslations,
                    media,
                    explanations,
                    language
                )
                ;
        }

        public override string SentenceToAskChatGptExplanation =>
            $"Peux-tu expliquer le sens du mot {this.Language.GetLanguageInFrench()} \"{this.Word.Value}\" " +
            $"dans la phrase \"{this.OriginalSentences!.GetCentralSentence()}\"";

        public override string? ContentToGuessInTargetedLanguage() => this.TranslatedWord?.Value;
        public override string ContentToGuessInNativeLanguage() => this.Word.Value;

        public override string Translation
        {
            set => this.TranslatedWord = TranslatedWord.Create(value);
        }
    }
}