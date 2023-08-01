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

}