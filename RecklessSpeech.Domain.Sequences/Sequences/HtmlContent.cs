namespace RecklessSpeech.Domain.Sequences.Sequences;

public record HtmlContent(string Value)
{
    public static HtmlContent Hydrate(string value) => new(value);

    public static HtmlContent Create(string value)
    {
        if (value.StartsWith("\"<style>") is false)
            throw new InvalidHtmlContentException();
        
        return new(value);
    }
}