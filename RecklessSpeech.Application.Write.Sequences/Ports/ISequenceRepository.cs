using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Ports
{
    public interface ISequenceRepository
    {
        Sequence? GetOne(Guid id);
        IEnumerable<Sequence> GetOneByMediaId(long mediaId);
        void Add(Sequence sequence);
    }
}