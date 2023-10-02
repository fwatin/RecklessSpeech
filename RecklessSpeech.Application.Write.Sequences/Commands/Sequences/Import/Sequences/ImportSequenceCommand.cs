namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Sequences
{
    public record ImportSequenceCommand(ImportSequenceDto Dto) : IRequest;

    public record TranslationDto(string?[]? HumanTranslation, string?[]? MachineTranslation);
    
    public record ImportSequenceDto
    (
        string Word,
        string[] TranslatedWordPropositions,
        string[] OriginalSentences,
        TranslationDto TranslatedSentence,
        string Title,
        long MediaId,
        string? LeftImage,
        string? RightImage
    );

}