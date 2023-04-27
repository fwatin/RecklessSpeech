using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Read.Queries
{
    public static class SequenceExtensions
    {
        public static SequenceSummaryQueryModel ToQueryModel(this Sequence sequence)
        {
            return new(
                sequence.SequenceId.Value,
                sequence.Word.Value,
                sequence.TranslatedWord == null ? "" : sequence.TranslatedWord.Value
            );
        }
        
        
    }
}