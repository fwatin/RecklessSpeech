using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.Import;

public class ImportSequencesCommandHandler
{
    public async Task<IReadOnlyCollection<IDomainEvent>> Handle(ImportSequencesCommand command)
    {
        List<IDomainEvent> events = new();
        IReadOnlyCollection<ImportSequenceDto> lines = this.Parse(command.FileContent);

        foreach (var line in lines)
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
        var elements = fileContent.Split("	");
        List<ImportSequenceDto> dtos = new()
        {
            new ImportSequenceDto(elements[0],
                ParseAudioFileName(elements[1]),
                elements[2])
        };

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