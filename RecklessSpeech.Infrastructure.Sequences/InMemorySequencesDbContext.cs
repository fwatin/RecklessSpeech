using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Infrastructure.Sequences;

public class InMemorySequencesDbContext : ISequencesDbContext
{
    public InMemorySequencesDbContext()
    {
        this.Explanations = new();
        this.Sequences = new();
        this.LanguageDictionaries = new();

        Initialise();
    }
    private void Initialise()
    {
        this.LanguageDictionaries.Add(new()
        {
            Id = Guid.Parse("1224B241-1368-4AF6-B88B-DFA65E8CD232"),
            Url = $"https://www.wordreference.com/enfr/{1}"
        });
    }

    public List<SequenceEntity> Sequences { get; }
    public List<ExplanationEntity> Explanations { get; }
    public List<LanguageDictionaryEntity> LanguageDictionaries { get; set; }
}