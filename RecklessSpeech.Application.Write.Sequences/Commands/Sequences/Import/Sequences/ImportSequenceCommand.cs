namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Sequences
{
    public record ImportSequenceCommand(string? Word,
        string[] TranslatedWordPropositions,
        string[] OriginalSentences,
        string?[]? HumanTranslation, string?[]? MachineTranslation,
        string Title,
        long MediaId,
        string? LeftImage,
        string? RightImage,
        string? Mp3) : IRequest;
}