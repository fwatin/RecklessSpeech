namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Sequences
{
    public record ImportSequenceCommand(
        string Word,
        string OriginalSentence,
        string TranslatedSentence,
        string Title,
        long MediaId) : IRequest;

}