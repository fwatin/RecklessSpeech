using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Infrastructure.Sequences;

public class InMemorySequencesDbContext : ISequencesDbContext
{
    public InMemorySequencesDbContext()
    {
        Sequences = new List<SequenceEntity>();
    }

    public List<SequenceEntity> Sequences { get; }
}