using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;

namespace RecklessSpeech.Application.Read.Ports;

public interface ISequenceQueryRepository
{
    Task<IReadOnlyCollection<SequenceSummaryQueryModel>> GetAll();
    Task<SequenceSummaryQueryModel?> TryGetOne(Guid id);
}