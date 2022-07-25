using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Infrastructure.Databases;

public class RecklessSpeechDbContext
{
    public RecklessSpeechDbContext()
    {
        Sequences = new List<SequenceEntity>();
    }
    public List<SequenceEntity> Sequences { get; }
}