using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Shared.Tests.Explanations
{
    public record ExplanationBuilder
    {
        private ExplanationBuilder(
            ExplanationIdBuilder explanationId,
            ContentBuilder content,
            TargetBuilder target,
            SourceUrlBuilder sourceUrl
        )
        {
            this.ExplanationId = explanationId;
            this.Content = content;
            this.Target = target;
            this.SourceUrl = sourceUrl;
        }

        public ExplanationIdBuilder ExplanationId { get; init; }
        public ContentBuilder Content { get; init; }
        public TargetBuilder Target { get; init; }
        public SourceUrlBuilder SourceUrl { get; init; }

        public static ExplanationBuilder Create() =>
            new(
                new(),
                new(),
                new(),
                new());

        public static ExplanationBuilder Create(Guid id) =>
            new(
                new(id),
                new(),
                new(),
                new());

        public static implicit operator Explanation(ExplanationBuilder builder) =>
            Explanation.Create(
                builder.ExplanationId.Value,
                builder.Content.Value,
                builder.Target.Value,
                builder.SourceUrl.Value);

        public Explanation BuildDomain() =>
            Explanation.Hydrate(
                this.ExplanationId.Value,
                this.Content.Value,
                this.Target.Value,
                this.SourceUrl.Value);
    }
}