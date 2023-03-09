using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Domain.Sequences.Notes
{
    public record Question(string Value)
    {
        public static Question Create(HtmlContent htmlContent) => new(htmlContent.Value);
    }
}