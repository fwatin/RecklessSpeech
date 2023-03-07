using RecklessSpeech.Application.Read.Ports;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Infrastructure.Read.Tests.TestDoubles.Repositories;

public class InMemoryTestSequenceQueryRepository : ISequenceQueryRepository
{
    private readonly List<SequenceSummaryQueryModel> sequences;

    public InMemoryTestSequenceQueryRepository()
    {
        this.sequences = new();
    }

    public void Feed(SequenceSummaryQueryModel sequence) => this.sequences.Add(sequence);

    public async Task<IReadOnlyCollection<SequenceSummaryQueryModel>> GetAll()
    {
        return await Task.FromResult(sequences);
    }

    public async Task<SequenceSummaryQueryModel> GetOne(Guid id)
    {
        return await Task.FromResult(sequences.First(x => x.Id == id));
    }
}