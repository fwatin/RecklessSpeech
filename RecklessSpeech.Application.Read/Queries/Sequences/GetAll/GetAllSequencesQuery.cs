using RecklessSpeech.Application.Core.Queries;

namespace RecklessSpeech.Application.Read.Queries.Sequences.GetAll
{
    public record GetAllSequencesQuery :  IQuery<IReadOnlyCollection<SequenceSummaryQueryModel>>;
}