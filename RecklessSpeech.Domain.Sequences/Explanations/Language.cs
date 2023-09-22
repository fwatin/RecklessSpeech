namespace RecklessSpeech.Domain.Sequences.Explanations
{
    public abstract class Language
    {
        public abstract string GetLanguageInFrench();
    }

    public class English : Language
    {
        public override string GetLanguageInFrench() => "anglais";
    }
    
    public class Italian : Language
    {
        public override string GetLanguageInFrench() => "italien";
    }

    public class Dutch : Language
    {
        public override string GetLanguageInFrench() => "néérlandais";
    }
}