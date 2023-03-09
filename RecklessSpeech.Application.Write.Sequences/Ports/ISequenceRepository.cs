using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Ports
{
    public interface ISequenceRepository
    {
        Task<Sequence?> GetOne(Guid id);
        Task<Sequence?> GetOneByWord(string word);
    }
}