namespace RecklessSpeech.Infrastructure.Entities
{
    public record ExplanationDao : RootDao
    {
        public ExplanationDao(
            Guid id,
            string target,
            string content,
            string sourceUrl
        )
        {
            this.Id = id;
            this.Target = target;
            this.Content = content;
            this.SourceUrl = sourceUrl;
        }

        public string Target { get; } = default!;
        public string Content { get; } = default!;
        public string SourceUrl { get; } = default!;
    }
}