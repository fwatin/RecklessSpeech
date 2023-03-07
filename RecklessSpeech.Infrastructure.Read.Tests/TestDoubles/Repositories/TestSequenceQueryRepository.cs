using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;

namespace RecklessSpeech.Infrastructure.Read.Tests.TestDoubles.Repositories
{
    public class InMemoryTestSequenceQueryRepository : ISequenceQueryRepository
    {
        private readonly List<SequenceSummaryQueryModel> sequences;

        public InMemoryTestSequenceQueryRepository() => this.sequences = new();

        public async Task<IReadOnlyCollection<SequenceSummaryQueryModel>> GetAll() =>
            await Task.FromResult(this.sequences);

        public async Task<SequenceSummaryQueryModel> GetOne(Guid id) =>
            await Task.FromResult(this.sequences.First(x => x.Id == id));

        public void Feed(SequenceSummaryQueryModel sequence) => this.sequences.Add(sequence);
    }
}