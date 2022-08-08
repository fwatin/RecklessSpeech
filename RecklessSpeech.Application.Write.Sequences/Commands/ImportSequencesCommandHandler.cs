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
            var sequence = Sequence.Create(Guid.NewGuid(),
                HtmlContent.Create(line.HtmlContent),
                AudioFileNameWithExtension.Create(line.AudioFileNameWithExtension),
                Tags.Create(line.Tags));

            events.AddRange(sequence.Import());
        }

        return await Task.FromResult(events);
    }

    private IReadOnlyCollection<ImportSequenceDto> Parse(string fileContent)
    {
        string delimiter = "\"<style>";
        string[] lines = fileContent.Split(delimiter);
        List<ImportSequenceDto> dtos = new();

        for (int i = 1; i < lines.Length; i++)
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
        if (element.StartsWith("\"")) element = element.Substring(1, element.Length - 1);
        if (element.EndsWith("\"")) element = element.Substring(0, element.Length - 1);
        element = element.Replace("\"\"", "\"");
        return element;
    }

    private string ParseAudioFileName(string audioFileNameWithContext)
    {
        int leftPartLength = "[sound:".Length;
        int rightPartLength = "]".Length;
        return audioFileNameWithContext.Substring(leftPartLength,
            audioFileNameWithContext.Length - leftPartLength - rightPartLength);
    }

    private record ImportSequenceDto(string HtmlContent, string AudioFileNameWithExtension, string Tags);
}