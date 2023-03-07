namespace RecklessSpeech.Domain.Sequences.Explanations;

public sealed class Explanation
{
    public Content Content { get; set; }
    public Target Target { get; set; }
    public ExplanationId ExplanationId { get; init; }
    public SourceUrl SourceUrl { get; set; }

    private Explanation(ExplanationId explanationId, Content content, Target target, SourceUrl sourceUrl)
    {
        this.ExplanationId = explanationId;
        this.Content = content;
        this.Target = target;
        this.SourceUrl = sourceUrl;

    }
    public static Explanation Create(Guid id, string content, string target, string sourceUrl)
    {
        return new(
            new(id),
            new(content),
            new(target),
            new(sourceUrl));
    }

    public static Explanation Hydrate(Guid id, string content, string target, string sourceUrl)
    {
        return new(
            new(id),
            new(content),
            new(target),
            new(sourceUrl));
    }
}