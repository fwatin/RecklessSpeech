namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Sequences
{
    public record ImportWordCommand(string? Word,
        string[] WordTranslations,
        string[] OriginalSentences,
        string?[]? HumanTranslation,
        string?[]? MachineTranslation,
        string Title,
        long MediaId,
        string? LeftImageBase64,
        string? RightImageBase64,
        string? Mp3Base64,
        string LanguageCode) : IRequest;
}