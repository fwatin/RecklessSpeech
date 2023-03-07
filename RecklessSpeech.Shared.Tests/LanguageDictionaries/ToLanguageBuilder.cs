namespace RecklessSpeech.Shared.Tests.LanguageDictionaries
{
    public class ToLanguageBuilder
    {
        public ToLanguageBuilder()
        {
        }

        public ToLanguageBuilder(string value) => this.Value = value;

        public string Value { get; } = "French";
    }
}