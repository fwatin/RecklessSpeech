using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Infrastructure.Sequences;

public interface ISequencesDbContext
{
    List<SequenceEntity> Sequences { get; }
}