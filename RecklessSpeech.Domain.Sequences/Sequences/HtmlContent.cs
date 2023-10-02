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

        public static HtmlContent Create(Media media, OriginalSentences originalSentences, Word word, string title)
        {
            StringBuilder stringBuilder = new();
            string pattern = $"({Regex.Escape(word.Value)})";
            var splittedSentenceKeepingSeparator = Regex.Split(originalSentences.GetCentralSentence(),
                pattern,
                RegexOptions.IgnoreCase);

            foreach (var wordInSentence in splittedSentenceKeepingSeparator)
            {
                if (wordInSentence.ToLowerInvariant().StartsWith(word.Value.ToLowerInvariant()))
                {
                    string underlined =
                        $"<span class=\"dc-gap\"><span class=\"dc-down dc-lang-en dc-orig\"" +
                        $" style=\"background-color: rgb(157, 0, 0);\">{wordInSentence}</span></span>";
                    stringBuilder.Append(underlined);
                }
                else
                {
                    string normal = $"<span class=\"dc-down dc-lang-en dc-orig\">{wordInSentence}</span>";
                    stringBuilder.Append(normal);
                }
            }

            string sentence = stringBuilder.ToString();

            string template = GetTemplate();

            string html = template.Replace("{{mediaId}}", media.MediaId.ToString());
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