using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences;

public class HtmlContentBuilder
{
    public HtmlContentBuilder() { }
    public HtmlContentBuilder(string value)
    {
        this.Value = value;
    }
    
    public string Value { get; set; } = "default value for HtmlContent";

    public static implicit operator HtmlContent(HtmlContentBuilder builder) => new(builder.Value);
}