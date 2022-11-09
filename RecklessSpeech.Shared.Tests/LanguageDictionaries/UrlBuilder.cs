namespace RecklessSpeech.Shared.Tests.LanguageDictionaries;

public class UrlBuilder
{
    public UrlBuilder()
    {
    }
    public UrlBuilder(string value)
    {
        this.Value = value;
    }
    
    public string Value { get; } = $"https://www.wordreference.com/enfr/{1}";
}