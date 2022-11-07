namespace RecklessSpeech.Shared.Tests.LanguageDictionaries;

public class NameBuilder
{
    public NameBuilder()
    {
    }
    public NameBuilder(string value)
    {
        this.Value = value;
    }
    
    public string Value { get; } = $"WordReference";
}