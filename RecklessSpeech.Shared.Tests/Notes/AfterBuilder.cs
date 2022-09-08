using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Shared.Tests.Notes;

public class AfterBuilder
{
    public AfterBuilder() { }
    public AfterBuilder(string value)
    {
        this.Value = value;
    }
    
    public string Value { get; set; } = "default value for After";

    public static implicit operator After(AfterBuilder builder) => new(builder.Value);
}