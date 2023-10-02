namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Sequences
{
    public record ImportSequenceCommand(
        string Word,
        string[] TranslatedWordPropositions,
        string[] OriginalSentences,
        TranslationDto TranslatedSentence,
        string Title,
        long MediaId) : IRequest;

    public record TranslationDto(string?[]? HumanTranslation, string?[]? MachineTranslation);
    
    public record ImportSequenceDto
    (
        string? LeftImage,
        string? RightImage,
        long TimeModified,
        TranslationDto Translation,
        string formText,
        string[] formTextTranslation,
        string Title
    );

}