namespace RecklessSpeech.Domain.Sequences.Explanations
{
    public sealed class Explanation
    {
        private Explanation(ExplanationId explanationId, Content content, Target target, SourceUrl sourceUrl)
        {
            this.ExplanationId = explanationId;
            this.Content = content;
            this.Target = target;
            this.SourceUrl = sourceUrl;
        }

        public Content Content { get; init; }
        public Target Target { get; init; }
        public ExplanationId ExplanationId { get; init; }
        public SourceUrl SourceUrl { get; init; }

        public static Explanation Create(Guid id, string content, string target, string sourceUrl) =>
            new(
                new(id),
                new(content),
                new(target),
                new(sourceUrl));

        public static Explanation Hydrate(Guid id, string content, string target, string sourceUrl) =>
            new(
                new(id),
                new(content),
                new(target),
                new(sourceUrl));
    }
}