using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Infrastructure.Sequences.Repositories
{
    public class InMemoryDataContext : IDataContext
    {
        public InMemoryDataContext()
        {
            this.Explanations = new();
            this.Sequences = new();
        }

        public List<SequenceDao> Sequences { get; }
        public List<ExplanationDao> Explanations { get; }
    }
}