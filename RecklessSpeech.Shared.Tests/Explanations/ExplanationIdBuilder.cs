using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Shared.Tests.Explanations;

public class ExplanationIdBuilder
{
    public ExplanationIdBuilder() { }
    public ExplanationIdBuilder(Guid value)
    {
        this.Value = value;
    }
    
    public Guid Value { get; set; } = Guid.Parse("6BE302CE-A115-428B-BE0C-F37132F82E9B");

    public static implicit operator ExplanationId(ExplanationIdBuilder builder) => new(builder.Value);
}