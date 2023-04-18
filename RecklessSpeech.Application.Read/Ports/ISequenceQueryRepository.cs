using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Read.Ports
{
    public interface ISequenceQueryRepository
    {
        IReadOnlyCollection<Sequence> GetAll();
        Sequence? GetOne(Guid id);
    }
}