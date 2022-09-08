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
        IReadOnlyCollection<ImportSequenceDto> lines = this.Parse(command.FileContent);

        foreach (ImportSequenceDto line in lines)
        {
            HtmlContent? htmlContent = HtmlContent.Create(line.RawHtml);
            (Word, TranslatedSentence) data = GetDataFromHtml(htmlContent);

            Sequence? sequence = Sequence.Create(Guid.NewGuid(),
                htmlContent,
                AudioFileNameWithExtension.Create(line.AudioFileNameWithExtension),
                GetTags(line.Tags),
                data.Item1,
                data.Item2);

            events.AddRange(sequence.Import());
        }

        return await Task.FromResult(events);
    }

    private static (Word,TranslatedSentence) GetDataFromHtml(HtmlContent htmlContent)
    {
        HtmlDocument htmlDoc = new();
        htmlDoc.LoadHtml(htmlContent.Value);
        
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

        return (word,translatedSentence);
    }

    private Tags GetTags(string element)
    {
        if (element.StartsWith("\""))
            element = element.Substring(1,
                element.Length - 1);
        if (element.EndsWith("\""))
            element = element.Substring(0,
                element.Length - 1);
        return Tags.Create(element.Trim());
    }

    private IReadOnlyCollection<ImportSequenceDto> Parse(string fileContent)
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

    private string ParseHtmlContent(string element)
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

    private string ParseAudioFileName(string audioFileNameWithContext)
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