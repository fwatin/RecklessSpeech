using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences;

public class LanguageDictionaryIdBuilder
{
    public LanguageDictionaryIdBuilder()
    {
    }
    public LanguageDictionaryIdBuilder(Guid value)
    {
        this.Value = value;
    }
    
    public Guid Value { get; } = Guid.Parse("176D9A71-7507-4997-B429-A9B148AF64E2");

    public static implicit operator LanguageDictionaryId?(LanguageDictionaryIdBuilder? builder)
    {
        if (builder is null) return null;
        return new(builder.Value);
    }
}