using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Infrastructure.Databases;

public class InMemoryRecklessSpeechDbContext
{
    public InMemoryRecklessSpeechDbContext()
    {
        Sequences = new List<SequenceEntity>();
    }
    public List<SequenceEntity> Sequences { get; }
}