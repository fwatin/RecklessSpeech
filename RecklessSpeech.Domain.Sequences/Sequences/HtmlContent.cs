using System.Text;
using System.Text.RegularExpressions;

namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public record HtmlContent(string Value)
    {
        public static HtmlContent Hydrate(string value) => new(value);

        public static HtmlContent Create(string value)
        {
            if (value.StartsWith("<style>") is false)
            {
                throw new InvalidHtmlContentException();
            }

            return new(value);
        }

        public static HtmlContent Create(MediaId mediaId, TranslatedSentence translatedSentence, Word word, string title)
        {
            StringBuilder stringBuilder = new();
            string pattern = $"({Regex.Escape(word.Value)})";
            var splittedSentenceKeepingSeparator = Regex.Split(translatedSentence.Value, pattern); 

            foreach (var wordInSentence in splittedSentenceKeepingSeparator)
            {
                if (wordInSentence.Trim().StartsWith(word.Value))
                {
                    string underlined =
                        $"<span class=\"dc-gap\"><span class=\"dc-down dc-lang-en dc-orig\"" +
                        $" style=\"background-color: rgb(157, 0, 0);\">{wordInSentence}</span></span>";
                    stringBuilder.AppendLine(underlined);
                }
                else
                {
                    string normal = $"<span class=\"dc-down dc-lang-en dc-orig\">{wordInSentence}</span>";
                    stringBuilder.AppendLine(normal);
                }
            }

            string sentence = stringBuilder.ToString();

            string template = GetTemplate();

            string html = template.Replace("{{mediaId}}", mediaId.Value.ToString());
            html = html.Replace("{{html_sentence}}", sentence);
            html = html.Replace("{{title}}", title);

            return new(html);
        }
        private static string GetTemplate()
        {
            string path = Path.Join(AppContext.BaseDirectory, "Sequences", "sequence_template.html");
            return File.ReadAllText(path);
        }
    }
}