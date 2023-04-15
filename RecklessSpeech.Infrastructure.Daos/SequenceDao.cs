﻿namespace RecklessSpeech.Infrastructure.Entities
{
    public record SequenceDao : RootDao
    {
        public SequenceDao(
            Guid id,
            string htmlContent,
            string audioFileNameWithExtension,
            string tags,
            string word,
            Guid? explanationId,
            string translatedSentence,
            string? translatedWord,
            long mediaId
        )
        {
            this.Id = id;
            this.HtmlContent = htmlContent;
            this.AudioFileNameWithExtension = audioFileNameWithExtension;
            this.Tags = tags;
            this.Word = word;
            this.ExplanationId = explanationId;
            this.TranslatedSentence = translatedSentence;
            this.TranslatedWord = translatedWord;
            this.MediaId = mediaId;
        }

        public string HtmlContent { get; } = default!;
        public string AudioFileNameWithExtension { get; } = default!;
        public string Tags { get; } = default!;
        public string Word { get; } = default!;
        public Guid? ExplanationId { get; set; }
        public string TranslatedSentence { get; } = default!;
        public string? TranslatedWord { get; set; }
        public long MediaId { get; set; }
    }
}