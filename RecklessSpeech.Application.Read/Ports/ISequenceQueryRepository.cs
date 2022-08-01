using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;

namespace RecklessSpeech.Application.Read.Ports;

public interface ISequenceQueryRepository
{
    Task<IReadOnlyCollection<SequenceSummaryQueryModel>> GetAll();
    SequenceSummaryQueryModel? TryGetOne(Guid id); //todo check if same query model to be returned
}