using ExCSS;
using HtmlAgilityPack;
using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Core.Events;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Sequences;
using System.Text;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import
{
    public record ImportSequencesCommand(string FileContent) : IEventDrivenCommand;

    public class ImportSequencesCommandHandler : CommandHandlerBase<ImportSequencesCommand>
    {
        private readonly ISequenceRepository sequenceRepository;

        public ImportSequencesCommandHandler(ISequenceRepository sequenceRepository)
        {
            this.sequenceRepository = sequenceRepository;
        }

        protected override async Task<IReadOnlyCollection<IDomainEvent>> Handle(ImportSequencesCommand command)
        {
            if (command.FileContent.StartsWith("\"<style>") is false)
            {
                throw new InvalidHtmlContentException();
            }

            List<IDomainEvent> events = new();
            IEnumerable<ImportSequenceDto> lines = Parse(command.FileContent);

            foreach ((string? rawHtml, string? audioFileNameWithExtension, string? tags) in lines)
            {
                (Word? word, TranslatedSentence? translatedSentence) = GetDataFromHtml(rawHtml);

                if (await this.AlreadyImported(word)) continue;

                HtmlContent htmlContent = GetHtmlContent(rawHtml, translatedSentence);

                Sequence sequence = Sequence.Create(Guid.NewGuid(),
                    htmlContent,
                    AudioFileNameWithExtension.Create(audioFileNameWithExtension),
                    GetTags(tags),
                    word,
                    translatedSentence,
                    this.GetMediaId(audioFileNameWithExtension));

                events.AddRange(sequence.Import());
            }

            return await Task.FromResult(events);
        }
        private MediaId GetMediaId(string audioFileNameWithExtension)
        {
            string? fileName = Path.GetFileName(audioFileNameWithExtension);
            return long.TryParse(fileName, out long value)
                ? new(value)
                : new MediaId(0);
        }

        private async Task<bool> AlreadyImported(Word word)
        {
            Sequence? sequence = await this.sequenceRepository.GetOneByWord(word.Value);
            return sequence is not null;
        }

        private static HtmlContent GetHtmlContent(string rawHtml, TranslatedSentence translatedSentence)
        {
            string html = RemoveGap(rawHtml);

            html = RemoveTranslatedSentence(html, translatedSentence);

            HtmlDocument htmlDoc = SetBackgroundToRedForWordNode(html);

            HtmlNode node = RemoveBackgroundInStyle(htmlDoc.DocumentNode);

            return HtmlContent.Create(node.InnerHtml);
        }

        private static HtmlNode RemoveBackgroundInStyle(HtmlNode node)
        {
            HtmlNode? nodeWithContent = node.Descendants("div").FirstOrDefault();
            if (nodeWithContent is null)
            {
                return node;
            }

            StylesheetParser parser = new();
            Stylesheet? stylesheet = parser.Parse(node.InnerHtml);

            List<IStyleRule> rules = stylesheet.StyleRules.ToList();

            IEnumerable<IStyleRule> toBeRemoved = rules.Where(x =>
                x.SelectorText == ".card" ||
                x.SelectorText == ".nightMode .dc-card" ||
                x.SelectorText == ".nightMode.card");

            IEnumerable<IStyleRule> newRules = rules.Except(toBeRemoved);

            StringBuilder styleBuilder = new();
            styleBuilder.AppendLine("<style>");
            styleBuilder.AppendLine("html, ");

            foreach (IStyleRule newRule in newRules)
            {
                string? ruleText = newRule.Text;
                if (newRule.SelectorText == ".dc-image")
                {
                    //with d?gag? par le parseur css probablement du au fait que ca utilise une fonction Js
                    //pas trouv? d'autre moyen que l'ajouter comme ca (newRule.Style.Width = ... marche pas)
                    ruleText = ruleText.Replace("{", "{width: calc(50% - 10px);");
                }

                styleBuilder.AppendLine(ruleText);
            }

            styleBuilder.AppendLine("</style>");

            string style = styleBuilder.ToString();
            return CreateNode(style, nodeWithContent);
        }

        private static HtmlNode CreateNode(string style, HtmlNode content)
        {
            HtmlDocument htmlDocument = new();
            htmlDocument.DocumentNode.AddClass("dc-bg");

            htmlDocument.DocumentNode.AppendChild(HtmlNode.CreateNode(style));

            string div = "<div class=\"dc-bg\"> " + content.InnerHtml + "</div>";
            htmlDocument.DocumentNode.AppendChild(HtmlNode.CreateNode(div));

            return htmlDocument.DocumentNode;
        }

        private static string RemoveGap(string html)
        {
            html = html.Replace("{{c1::", "");
            html = html.Replace("}}", "");
            return html;
        }

        private static string RemoveTranslatedSentence(string html, TranslatedSentence translatedSentence)
        {
            if (string.IsNullOrEmpty(translatedSentence.Value) is false)
            {
                html = html.Replace(translatedSentence.Value, "");
            }

            return html;
        }

        private static HtmlDocument SetBackgroundToRedForWordNode(string html)
        {
            HtmlDocument htmlDoc = new();
            htmlDoc.LoadHtml(html);

            HtmlNode? wordNode = htmlDoc.DocumentNode.Descendants()
                .FirstOrDefault(n => n.HasClass("dc-gap"));

            wordNode?.ChildNodes.Single().Attributes.Add("style", "background-color: rgb(157, 0, 0);");
            return htmlDoc;
        }

        private static (Word, TranslatedSentence) GetDataFromHtml(string rawHtml)
        {
            HtmlDocument htmlDoc = new();
            htmlDoc.LoadHtml(rawHtml);

            HtmlNode? wordNode = htmlDoc.DocumentNode.Descendants()
                .FirstOrDefault(n => n.HasClass("dc-gap"));
            Word word = Word.Create(wordNode != null
                ? wordNode.InnerText
                : "");


            HtmlNode? translatedSentenceNode = htmlDoc.DocumentNode.Descendants()
                .FirstOrDefault(n => n.HasClass("dc-translation"));

            TranslatedSentence translatedSentence = TranslatedSentence.Create(translatedSentenceNode != null
                ? translatedSentenceNode.InnerText
                : "");

            return (word, translatedSentence);
        }

        private static Tags GetTags(string element)
        {
            if (element.StartsWith("\""))
            {
                element = element.Substring(1,
                    element.Length - 1);
            }

            if (element.EndsWith("\n"))
            {
                element = element[..^1];
            }

            if (element.EndsWith("\""))
            {
                element = element[..^1];
            }

            return Tags.Create(element.Trim());
        }

        private static IEnumerable<ImportSequenceDto> Parse(string fileContent)
        {
            string delimiter = "\"<style>";
            string[] lines = fileContent.Split(delimiter);
            List<ImportSequenceDto> dtos = new();

            for (int i = 1;
                 i < lines.Length;
                 i++)
            {
                try
                {
                    string reconstitutedLine = delimiter + lines[i];
                    string[] elements = reconstitutedLine.Split("	");
                    dtos.Add(
                        new(
                            ParseHtmlContent(elements[0]),
                            ParseAudioFileName(elements[1]),
                            elements[2])
                    );
                }
                catch
                {
                    // ignored
                }
            }

            return dtos;
        }

        private static string ParseHtmlContent(string element)
        {
            if (element.StartsWith("\""))
            {
                element = element.Substring(1,
                    element.Length - 1);
            }

            if (element.EndsWith("\""))
            {
                element = element.Substring(0,
                    element.Length - 1);
            }

            element = element.Replace("\"\"",
                "\"");

            element = element.Replace("background-color: white;",
                "");

            return element;
        }

        private static string ParseAudioFileName(string audioFileNameWithContext)
        {
            int leftPartLength = "[sound:".Length;
            int rightPartLength = "]".Length;
            return audioFileNameWithContext.Substring(leftPartLength,
                audioFileNameWithContext.Length - leftPartLength - rightPartLength);
        }

        private record ImportSequenceDto(string RawHtml,
            string AudioFileNameWithExtension,
            string Tags);
    }
}