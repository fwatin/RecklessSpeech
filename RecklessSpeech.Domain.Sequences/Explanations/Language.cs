namespace RecklessSpeech.Domain.Sequences.Explanations
{
    public abstract class Language
    {
        public abstract string GetLanguageInFrench();

        public static Language GetLanguageFromCode(string requestLanguageCode)
        {
            switch (requestLanguageCode)
            {
                case "nl": return new Dutch();
                case "en": return new English();
                case "it": return new Italian();
            }

            throw new($"not supported language with language code : {requestLanguageCode}");
        }
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