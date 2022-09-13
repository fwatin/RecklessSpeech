using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;

namespace RecklessSpeech.Application.Read.Ports;

public interface ISequenceQueryRepository
{
    Task<IReadOnlyCollection<SequenceSummaryQueryModel>> GetAll();
    Task<SequenceSummaryQueryModel> GetOne(Guid sequenceIdValue);
    Task<SequenceSummaryQueryModel?> TryGetOne(Guid sequenceIdValue);
}