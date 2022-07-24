using Microsoft.EntityFrameworkCore;
using RecklessSpeech.Infrastructure.Databases;
using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Infrastructure.Sequences;

public class SequencesDbContext
{
    private readonly InMemoryRecklessSpeechDbContext dbContext;

    public SequencesDbContext(InMemoryRecklessSpeechDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public List<SequenceEntity> Sequences => dbContext.Sequences;
}