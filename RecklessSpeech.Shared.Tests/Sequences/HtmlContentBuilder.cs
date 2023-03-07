using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences
{
    public class HtmlContentBuilder
    {
        public HtmlContentBuilder()
        {
            string path = Path.Join(AppContext.BaseDirectory, "Sequences", "MoneyballHtmlContent.html");
            string someRealCaseHtmlContentForGimmicksInMoneyBall = File.ReadAllText(path);
            this.Value = someRealCaseHtmlContentForGimmicksInMoneyBall;
        }

        public HtmlContentBuilder(string value) => this.Value = value;

        public string Value { get; set; }

        public static implicit operator HtmlContent(HtmlContentBuilder builder) => new(builder.Value);
    }
}