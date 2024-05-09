namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Phrases
{
    public record ImportPhraseCommand(
        string? Phrase,
        string[] OriginalSentences,
        string?[]? HumanTranslation, 
        string?[]? MachineTranslation,
        string Title,
        long MediaId,
        string? LeftImageBase64,
        string? RightImageBase64,
        string? Mp3Base64) : IRequest;
}