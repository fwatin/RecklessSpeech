using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Infrastructure.Sequences.Repositories
{
    public class InMemorySequenceRepository : ISequenceRepository, ISequenceQueryRepository
    {
        private readonly List<Sequence> sequences;
        public IReadOnlyCollection<Sequence> All => this.sequences;

        public InMemorySequenceRepository()
        {
            this.sequences = new();
        }

        public void Add(Sequence sequence)
        {
            this.sequences.Add(sequence);
        }

        public IReadOnlyCollection<Sequence> GetAll() => this.sequences;

        public Sequence? GetOne(Guid id)
        {
            Sequence? entity = this.sequences.SingleOrDefault(x => x.SequenceId.Value == id);
            return entity ?? null;
        }

        public Sequence? GetOneByWord(string word)
        {
            Sequence? entity = this.sequences.SingleOrDefault(x => x.Word.Value == word);
            return entity ?? null;
        }

        public Sequence? GetOneByMediaId(long mediaId)
        {
            Sequence? entity = this.sequences.SingleOrDefault(x => x.MediaId.Value == mediaId);
            return entity ?? null;
        }
    }
}