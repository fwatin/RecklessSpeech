using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Infrastructure.Sequences
{
    public interface IDataContext
    {
        List<SequenceEntity> Sequences { get; }
        List<ExplanationEntity> Explanations { get; }
    }
}