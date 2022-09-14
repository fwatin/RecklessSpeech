using HtmlAgilityPack;
using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Application.Write.Sequences.Commands;

public record ImportSequencesCommand(string FileContent) : IEventDrivenCommand;

public class ImportSequencesCommandHandler : CommandHandlerBase<ImportSequencesCommand>
{
    protected override async Task<IReadOnlyCollection<IDomainEvent>> Handle(ImportSequencesCommand command)
    {
        if (command.FileContent.StartsWith("\"<style>") is false)
            throw new InvalidHtmlContentException();

        List<IDomainEvent> events = new();
        IEnumerable<ImportSequenceDto> lines = Parse(command.FileContent);

        foreach ((string? rawHtml, string? audioFileNameWithExtension, string? tags) in lines)
        {
            (Word? word, TranslatedSentence? translatedSentence) = GetDataFromHtml(rawHtml);

            HtmlContent? htmlContent = GetHtmlContent(rawHtml, translatedSentence);

            Sequence? sequence = Sequence.Create(Guid.NewGuid(),
                htmlContent,
                AudioFileNameWithExtension.Create(audioFileNameWithExtension),
                GetTags(tags),
                word,
                translatedSentence);

            events.AddRange(sequence.Import());
        }

        return await Task.FromResult(events);
    }

    private static HtmlContent GetHtmlContent(string rawHtml, TranslatedSentence translatedSentence)
    {
        string html = RemoveGap(rawHtml);

        html = RemoveTranslatedSentence(html, translatedSentence);

        HtmlDocument htmlDoc = SetBackgroundToRedForWordNode(html);

        return HtmlContent.Create(htmlDoc.DocumentNode.InnerHtml);
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
        Word? word = Word.Create(wordNode != null
            ? wordNode.InnerText
            : "");


        HtmlNode? translatedSentenceNode = htmlDoc.DocumentNode.Descendants()
            .FirstOrDefault(n => n.HasClass("dc-translation"));

        TranslatedSentence? translatedSentence = TranslatedSentence.Create(translatedSentenceNode != null
            ? translatedSentenceNode.InnerText
            : "");

        return (word, translatedSentence);
    }

    private static Tags GetTags(string element)
    {
        if (element.StartsWith("\""))
            element = element.Substring(1,
                element.Length - 1);

        if (element.EndsWith("\n"))
            element = element[..^1];

        if (element.EndsWith("\""))
            element = element[..^1];

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
            string reconstitutedLine = delimiter + lines[i];
            string[] elements = reconstitutedLine.Split("	");
            dtos.Add(
                new ImportSequenceDto(
                    ParseHtmlContent(elements[0]),
                    ParseAudioFileName(elements[1]),
                    elements[2])
            );
        }

        return dtos;
    }

    private static string ParseHtmlContent(string element)
    {
        if (element.StartsWith("\""))
            element = element.Substring(1,
                element.Length - 1);
        if (element.EndsWith("\""))
            element = element.Substring(0,
                element.Length - 1);
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