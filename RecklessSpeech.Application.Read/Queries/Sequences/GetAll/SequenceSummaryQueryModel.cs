namespace RecklessSpeech.Application.Read.Queries.Sequences.GetAll
{
    public record SequenceSummaryQueryModel(Guid Id, string Word, string TranslatedWord, bool HasExplanations, int SentToAnkiTimes, bool HasMediaComplete);
}