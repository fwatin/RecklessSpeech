using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Shared.Tests.Sequences;

namespace RecklessSpeech.Shared.Tests.Explanations
{
    public record ExplanationBuilder
    {
        private ExplanationBuilder(
            ContentBuilder content,
            SourceUrlBuilder sourceUrl,
            LanguageBuilder language
        )
        {
            this.Content = content;
            this.SourceUrl = sourceUrl;
            this.Language = language;
        }

        public ContentBuilder Content { get; init; }
        public SourceUrlBuilder SourceUrl { get; init; }
        public LanguageBuilder Language { get; init; }

        public static ExplanationBuilder Create() =>
            new(
                new(),
                new(),
                new());

        public static ExplanationBuilder Create(Guid id) =>
            new(
                new(),
                new(),
                new());

        public static implicit operator Explanation(ExplanationBuilder builder) =>
            Explanation.Create(
                builder.Content.Value,
                null,
                builder.SourceUrl.Value,
                builder.Language.Value);

        public Explanation BuildDomain() =>
            Explanation.Hydrate(
                this.Content.Value,
                null,
                this.SourceUrl.Value,
                this.Language.Value);
    }
}