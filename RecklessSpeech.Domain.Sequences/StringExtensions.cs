namespace RecklessSpeech.Domain.Sequences
{
    public static class StringExtensions
    {
        public static string WithoutSpaceAndReturns(this string text)
        {
            string result = text;
            result = result.Replace(" ", "");
            result = result.Replace("\r", "");
            result = result.Replace("\n", "");
            return result;
        }
    }

    public static class StringCompare
    {
        public static string WithoutSpaceAndReturns(string text)
        {
            string result = text;
            result = result.Replace(" ", "");
            result = result.Replace("\r", "");
            result = result.Replace("\n", "");
            return result;
        }
    }
}