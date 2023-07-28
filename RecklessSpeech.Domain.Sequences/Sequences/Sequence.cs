using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public sealed class Sequence
    {
        public AudioFileNameWithExtension AudioFile = default!;
        private Sequence(SequenceId sequenceId)
        {
            this.SequenceId = sequenceId;
            this.Explanations = new();
        }

        public SequenceId SequenceId { get; }
        public HtmlContent HtmlContent { get; private init; } = default!;
        public Word Word { get; private init; } = default!;
        public TranslatedSentence TranslatedSentence { get; private init; } = default!;
        public List<Explanation> Explanations { get; set; }
        public TranslatedWord? TranslatedWord { get; set; }
        public MediaId MediaId { get;  private init; }= default!;
        public OriginalSentences? OriginalSentences { get; set; }
        public int SentToAnkiTimes { get; set; }


        public static Sequence Create(Guid id,
            HtmlContent htmlContent,
            AudioFileNameWithExtension audioFileNameWithExtension,
            Word word,
            TranslatedWord translatedWord,
            OriginalSentences originalSentences,
            TranslatedSentence translatedSentence,
            MediaId mediaId)
        {
            return new(new(id))
            {
                HtmlContent = htmlContent,
                AudioFile = audioFileNameWithExtension,
                Word = word,
                TranslatedWord = translatedWord,
                OriginalSentences = originalSentences,
                TranslatedSentence = translatedSentence,
                MediaId = mediaId,
                Explanations = new(),
                SentToAnkiTimes = 0
            };
        }

        public static Sequence Hydrate(Guid id,
            string htmlContent,
            string audioFileNameWithExtension,
            string word,
            string translatedSentence,
            long mediaId,
            List<Explanation> explanations,
            string? translatedWord, 
            List<string> originalSentences)
        {
            return new(new(id))
            {
                HtmlContent = HtmlContent.Hydrate(htmlContent),
                AudioFile = AudioFileNameWithExtension.Hydrate(audioFileNameWithExtension),
                Word = Word.Hydrate(word),
                TranslatedSentence = TranslatedSentence.Hydrate(translatedSentence),
                Explanations = explanations,
                TranslatedWord = TranslatedWord.Hydrate(translatedWord),
                MediaId = MediaId.Hydrate(mediaId),
                OriginalSentences = OriginalSentences.Hydrate(originalSentences),
                SentToAnkiTimes = 0
            };
        }
    }
}