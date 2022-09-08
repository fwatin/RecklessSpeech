namespace RecklessSpeech.Domain.Sequences.Explanations;

public sealed class Explanation
{
    public Content Content { get; set; }
    public Target Target { get; set; }
    public ExplanationId ExplanationId { get; init; }

    private Explanation(ExplanationId explanationId, Content content, Target target)
    {
        this.ExplanationId = explanationId;
        this.Content = content;
        this.Target = target;

    }
    public static Explanation Create(Guid id, string content, string target)
    {
        return new Explanation(
            new(id),
            new(content),
            new(target));
    }
    
    public static Explanation Hydrate(Guid id, string content, string target)
    {
        return new Explanation(
            new(id),
            new(content),
            new(target));
    }
}