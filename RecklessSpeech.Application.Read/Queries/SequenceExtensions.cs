using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Read.Queries
{
    public static class SequenceExtensions
    {
        public static SequenceSummaryQueryModel ToSummaryQueryModel(this Sequence sequence)
        {
            return new(
                sequence.SequenceId.Value,
                sequence.Word.Value,
                sequence.TranslatedWord is null ? "" : sequence.TranslatedWord.Value,
                sequence.Explanations.Any(),
                sequence.SentToAnkiTimes,
                sequence.Media is not null && sequence.Media.IsComplete()
            );
        }
        
        
    }
}