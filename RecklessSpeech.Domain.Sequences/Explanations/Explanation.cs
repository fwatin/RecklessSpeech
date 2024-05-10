using System.Text.RegularExpressions;

namespace RecklessSpeech.Domain.Sequences.Explanations
{
    public sealed class Explanation
    {
        //todo virer id c'est un value object
        private Explanation(
            ExplanationId explanationId,
            ExplanationInHtml explanationInHtml,
            RawExplanation? rawExplanation,
            Target target,
            SourceUrl sourceUrl, Language language)
        {
            this.ExplanationId = explanationId;
            this.ExplanationInHtml = explanationInHtml;
            this.RawExplanation = rawExplanation;
            this.Target = target;
            this.SourceUrl = sourceUrl;
            this.Language = language;
        }

        public ExplanationInHtml ExplanationInHtml { get; set; }
        public RawExplanation? RawExplanation { get; init; }
        public Target Target { get; init; }
        public ExplanationId ExplanationId { get; init; }
        public SourceUrl SourceUrl { get; init; }
        public Language Language { get; }

        public static Explanation Create(Guid id, string explanationInHtml, string? rawExplanation, string target,
            string sourceUrl, Language language) =>
            new(
                new(id),
                new(explanationInHtml),
                rawExplanation is null ? null : new(rawExplanation),
                new(target),
                new(sourceUrl),
                language);

        public static Explanation Hydrate(Guid id, string explanationInHtml, string? rawExplanation, string target,
            string sourceUrl, Language language) =>
            new(
                new(id),
                new(explanationInHtml),
                rawExplanation is null ? null : new(rawExplanation),
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
                new(""),
                Language.GetLanguageFromCode("en")); //sale mais je m'en branle
        }

        public void HighlightTerm(string wordToHighlight)
        {
            var html = this.ExplanationInHtml.Value;

            var escapedWord = Regex.Escape(wordToHighlight);

            const string styleToApply = "background-color: rgb(157, 0, 0);";

            var replacement = $"<span style=\"{styleToApply}\">{wordToHighlight}</span>";

            var regex = new Regex($@"\b{escapedWord}\b", RegexOptions.IgnoreCase);
            html = regex.Replace(html, replacement);

            this.ExplanationInHtml = new(html);
        }
    }
}