using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Infrastructure.Sequences;

public class InMemorySequencesDbContext : ISequencesDbContext
{
    public InMemorySequencesDbContext()
    {
        this.Explanations = new();
        this.Sequences = new();
    }

    public List<SequenceEntity> Sequences { get; }
    public List<ExplanationEntity> Explanations { get; }
}