using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Sequences;

namespace RecklessSpeech.Infrastructure.Databases;

public class InMemorySequencesDbContext : ISequencesDbContext
{
    public InMemorySequencesDbContext()
    {
        Sequences = new List<SequenceEntity>();
    }

    public List<SequenceEntity> Sequences { get; }
}