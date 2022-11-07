using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Infrastructure.Sequences;

public class InMemorySequencesDbContext : ISequencesDbContext
{
    public InMemorySequencesDbContext()
    {
        this.Explanations = new();
        this.Sequences = new();
        this.LanguageDictionaries = new();
    }

    public List<SequenceEntity> Sequences { get; }
    public List<ExplanationEntity> Explanations { get; }
    public List<LanguageDictionaryEntity> LanguageDictionaries { get; set; }
}