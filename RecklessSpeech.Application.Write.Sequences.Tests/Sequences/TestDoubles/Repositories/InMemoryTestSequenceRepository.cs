using RecklessSpeech.Application.Write.Sequences.Ports;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Repositories
{
    public class InMemoryTestSequenceRepository : ISequenceRepository
    {
        private readonly List<Sequence> sequences;

        public InMemoryTestSequenceRepository() => this.sequences = new();

        public IReadOnlyCollection<Sequence> All => this.sequences;

        public async Task<Sequence?> GetOne(Guid id) =>
            await Task.FromResult(this.sequences.SingleOrDefault(x => x.SequenceId.Value == id));

        public async Task<Sequence?> GetOneByWord(string word) =>
            await Task.FromResult(this.sequences.SingleOrDefault(x => x.Word.Value == word));
        public async Task<Sequence?> GetOneByMediaId(long mediaId)
        =>await Task.FromResult(this.sequences.SingleOrDefault(x => x.MediaId.Value == mediaId));


        public void Feed(Sequence sequence) => this.sequences.Add(sequence);
    }
}