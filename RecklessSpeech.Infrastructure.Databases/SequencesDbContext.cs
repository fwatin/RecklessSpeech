using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Sequences;

namespace RecklessSpeech.Infrastructure.Databases;

public class SequencesDbContext : ISequencesDbContext
{
    private readonly InMemoryRecklessSpeechDbContext dbContext;

    public SequencesDbContext(InMemoryRecklessSpeechDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public List<SequenceEntity> Sequences => dbContext.Sequences;
}