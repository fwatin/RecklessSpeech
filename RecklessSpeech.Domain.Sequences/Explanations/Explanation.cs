namespace RecklessSpeech.Domain.Sequences.Explanations
{
    public sealed class Explanation
    {
        //todo virer id c'est un value object
        private Explanation(ExplanationId explanationId, Content content, Target target, SourceUrl sourceUrl, Language language)
        {
            this.ExplanationId = explanationId;
            this.Content = content;
            this.Target = target;
            this.SourceUrl = sourceUrl;
            this.Language = language;
        }

        public Content Content { get; init; }
        public Target Target { get; init; }
        public ExplanationId ExplanationId { get; init; }
        public SourceUrl SourceUrl { get; init; }
        public Language Language { get; }

        public static Explanation Create(Guid id, string content, string target, string sourceUrl,Language language) =>
            new(
                new(id),
                new(content),
                new(target),
                new(sourceUrl),
                language);

        public static Explanation Hydrate(Guid id, string content, string target, string sourceUrl, Language language) =>
            new(
                new(id),
                new(content),
                new(target),
                new(sourceUrl),
                language);

        public static Explanation CreateEmpty()
        {
            return new Explanation
            (new(Guid.Empty),
                new(""),
                new(""),
                new(""),
                Language.GetLanguageFromCode("en")); //sale mais je m'en branle
        }
    }
}