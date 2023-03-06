using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Repositories;

public class InMemoryTestSequenceRepository : ISequenceRepository
{
    private readonly List<Sequence> sequences;

    public InMemoryTestSequenceRepository()
    {
        this.sequences = new();
    }

    public void Feed(Sequence sequence) => this.sequences.Add(sequence);

    public async Task<Sequence?> GetOne(Guid id)
    {
        return await Task.FromResult(sequences.Single(x => x.SequenceId.Value == id));
    }

    public async Task<Sequence?> GetOneByWord(string word)
    {
        return await Task.FromResult(sequences.Single(x => x.Word.Value == word));
    }
}