using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences;

public class HtmlContentBuilder
{
    public HtmlContentBuilder()
    {
        var path = Path.Join(AppContext.BaseDirectory,"Sequences","MoneyballHtmlContent.html");
        var someRealCaseHtmlContentForGimmicksInMoneyBall = File.ReadAllText(path);
        Value = someRealCaseHtmlContentForGimmicksInMoneyBall;
    }
    public HtmlContentBuilder(string value)
    {
        this.Value = value;
    }
    
    public string Value { get; set; }

    public static implicit operator HtmlContent(HtmlContentBuilder builder) => new(builder.Value);
}