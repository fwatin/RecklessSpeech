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
                sequence.ContentToGuessInNativeLanguage(),
                sequence.ContentToGuessInTargetedLanguage() is null ? "" : sequence.ContentToGuessInTargetedLanguage()!,
                sequence.Explanations.Any(),
                sequence.SentToAnkiTimes,
                sequence.Media is not null && sequence.Media.IsComplete()
            );
        }
    }
}