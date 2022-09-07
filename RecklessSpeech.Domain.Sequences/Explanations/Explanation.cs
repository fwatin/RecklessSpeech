namespace RecklessSpeech.Domain.Sequences.Explanations;

public sealed class Explanation
{
    public Content Content { get; set; }
    public Word Word { get; set; }
    public ExplanationId ExplanationId { get; init; }

    private Explanation(ExplanationId explanationId, Content content, Word word)
    {
        this.ExplanationId = explanationId;
        this.Content = content;
        this.Word = word;

    }
    public static Explanation Create(Guid id, string value, string word)
    {
        return new Explanation(
            new(id),
            new(value),
            new(word));
    }
}