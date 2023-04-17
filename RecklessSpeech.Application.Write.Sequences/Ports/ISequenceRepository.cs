using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Ports
{
    public interface ISequenceRepository
    {
        Sequence? GetOne(Guid id);
        Sequence? GetOneByWord(string word);
        Sequence? GetOneByMediaId(long mediaId);
    }
}