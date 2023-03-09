using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Infrastructure.Sequences.Repositories
{
    public interface IDataContext
    {
        List<SequenceDao> Sequences { get; }
        List<ExplanationDao> Explanations { get; }
    }
}