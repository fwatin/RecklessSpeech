using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Shared.Tests.Sequences;

namespace RecklessSpeech.Shared.Tests.Explanations
{
    public record ExplanationBuilder
    {
        private ExplanationBuilder(
            ExplanationIdBuilder explanationId,
            ContentBuilder content,
            TargetBuilder target,
            SourceUrlBuilder sourceUrl,
            LanguageBuilder language
        )
        {
            this.ExplanationId = explanationId;
            this.Content = content;
            this.Target = target;
            this.SourceUrl = sourceUrl;
            this.Language = language;
        }

        public ExplanationIdBuilder ExplanationId { get; init; }
        public ContentBuilder Content { get; init; }
        public TargetBuilder Target { get; init; }
        public SourceUrlBuilder SourceUrl { get; init; }
        public LanguageBuilder Language { get; init; }

        public static ExplanationBuilder Create() =>
            new(
                new(),
                new(),
                new(),
                new(),
                new());

        public static ExplanationBuilder Create(Guid id) =>
            new(
                new(id),
                new(),
                new(), 
                new(),
                new());

        public static implicit operator Explanation(ExplanationBuilder builder) =>
            Explanation.Create(
                builder.ExplanationId.Value,
                builder.Content.Value,
                builder.Target.Value,
                builder.SourceUrl.Value,
                builder.Language.Value);

        public Explanation BuildDomain() =>
            Explanation.Hydrate(
                this.ExplanationId.Value,
                this.Content.Value,
                this.Target.Value,
                this.SourceUrl.Value,
                this.Language.Value);
    }
}