using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Shared.Tests.Notes;

public class SourceBuilder
{
    public SourceBuilder() { }
    public SourceBuilder(string value)
    {
        this.Value = value;
    }
    
    public string Value { get; set; } = "to be replaced by local url"; //todo
    

    public static implicit operator Source(SourceBuilder builder) => new(builder.Value);
}