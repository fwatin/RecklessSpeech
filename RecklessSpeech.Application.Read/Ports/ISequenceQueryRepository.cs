using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;

namespace RecklessSpeech.Application.Read.Ports;

public interface ISequenceQueryRepository
{
    Task<IReadOnlyCollection<SequenceQueryModel>> GetAll();
}