namespace RecklessSpeech.Infrastructure.Entities
{
    public record SequenceEntity : AggregateRootEntity
    {
        public SequenceEntity(
            Guid id,
            string htmlContent,
            string audioFileNameWithExtension,
            string tags,
            string word,
            Guid? explanationId,
            string translatedSentence,
            string? translatedWord
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
        }

        public string HtmlContent { get; } = default!;
        public string AudioFileNameWithExtension { get; } = default!;
        public string Tags { get; } = default!;
        public string Word { get; } = default!;
        public Guid? ExplanationId { get; set; }
        public string TranslatedSentence { get; } = default!;
        public Guid? LanguageDictionaryId { get; } = default!;
        public string? TranslatedWord { get; set; } = default!;
    }
}