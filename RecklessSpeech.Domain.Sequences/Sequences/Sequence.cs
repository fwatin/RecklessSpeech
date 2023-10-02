﻿#nullable enable
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
        public SentenceTranslations SentenceTranslations { get; private init; } = default!;
        public List<Explanation> Explanations { get; set; }
        public TranslatedWord? TranslatedWord { get; set; }
        public MediaId? MediaId { get;  private init; }= default!;
        public OriginalSentences? OriginalSentences { get; set; }
        public int SentToAnkiTimes { get; set; }


        public static Sequence Create(Guid id,
            HtmlContent htmlContent,
            AudioFileNameWithExtension audioFileNameWithExtension,
            Word word,
            TranslatedWord translatedWord,
            OriginalSentences originalSentences,
            SentenceTranslations sentenceTranslations,
            MediaId mediaId,
            List<Explanation> explanations)
        {
            return new(new(id))
            {
                HtmlContent = htmlContent,
                AudioFile = audioFileNameWithExtension,
                Word = word,
                TranslatedWord = translatedWord,
                OriginalSentences = originalSentences,
                SentenceTranslations = sentenceTranslations,
                MediaId = mediaId,
                Explanations = explanations,
                SentToAnkiTimes = 0
            };
        }
    }
}