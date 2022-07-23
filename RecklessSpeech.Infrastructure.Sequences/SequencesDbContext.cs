using Microsoft.EntityFrameworkCore;
using RecklessSpeech.Infrastructure.Databases;
using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Infrastructure.Sequences;

public class SequencesDbContext
{
    private readonly RecklessSpeechDbContext dbContext;

    public SequencesDbContext(RecklessSpeechDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public List<SequenceEntity> Sequences => dbContext.Sequences;
}