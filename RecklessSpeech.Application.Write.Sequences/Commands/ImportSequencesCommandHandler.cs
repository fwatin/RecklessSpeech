using RecklessSpeech.Application.Core;
using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Domain.Sequences;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Application.Write.Sequences.Commands;

public record ImportSequencesCommand(string FileContent) : IEventDrivenCommand;

public class ImportSequencesCommandHandler : CommandHandlerBase<ImportSequencesCommand>
{
    protected override async Task<IReadOnlyCollection<IDomainEvent>> Handle(ImportSequencesCommand command)
    {
        List<IDomainEvent> events = new();
        IReadOnlyCollection<ImportSequenceDto> lines = this.Parse(command.FileContent);

        foreach (ImportSequenceDto line in lines)
        {
            events.Add
            (
                new SequencesImportRequestedEvent(line.HtmlContent,
                    AudioFileNameWithExtension.Create(line.AudioFileNameWithExtension), line.Tags)
            );
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
                new ImportSequenceDto(elements[0],
                    ParseAudioFileName(elements[1]),
                    elements[2])
            );
        }

        return dtos;
    }

    private string ParseAudioFileName(string audioFileNameWithContext)
    {
        int leftUnchallengeable = "[sound:".Length;
        return audioFileNameWithContext.Substring("[sound:".Length,
            audioFileNameWithContext.Length - leftUnchallengeable - 1);
    }

    private record ImportSequenceDto(string HtmlContent, string AudioFileNameWithExtension, string Tags);
}