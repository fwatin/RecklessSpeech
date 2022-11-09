namespace RecklessSpeech.Shared.Tests.LanguageDictionaries;

public class FromLanguageBuilder
{
    public FromLanguageBuilder()
    {
    }
    public FromLanguageBuilder(string value)
    {
        this.Value = value;
    }
    
    public string Value { get; } = "English";
}