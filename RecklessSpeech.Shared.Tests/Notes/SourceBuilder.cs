using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Shared.Tests.Notes;

public class SourceBuilder
{
    public SourceBuilder() { }
    public SourceBuilder(string value)
    {
        this.Value = value;
    }
    
    public string Value { get; set; } = "https://www.mijnwoordenboek.nl/vertaal/NL/FR/gimmicks";
    

    public static implicit operator Source(SourceBuilder builder) => new(builder.Value);
}