namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Sequences
{
    public record ImportSequenceCommand(
        string Word,
        string[] TranslatedWordPropositions,
        string[] OriginalSentences,
        string TranslatedSentence,
        string Title,
        long MediaId) : IRequest;

}