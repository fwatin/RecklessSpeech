﻿#nullable enable
using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public sealed class WordSequence : Sequence
    {
        public AudioFileNameWithExtension AudioFile = default!;

        private WordSequence(SequenceId sequenceId) : base(sequenceId)
        {
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
            return new(new(id))
            {
                HtmlContent = htmlContent,
                AudioFile = audioFileNameWithExtension,
                Word = word,
                TranslatedWord = translatedWord,
                OriginalSentences = originalSentences,
                SentenceTranslations = sentenceTranslations,
                Media = media,
                Explanations = explanations,
                SentToAnkiTimes = 0,
                Language = language
            };
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