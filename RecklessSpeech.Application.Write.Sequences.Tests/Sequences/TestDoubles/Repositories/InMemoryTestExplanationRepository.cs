using RecklessSpeech.Application.Write.Sequences.Ports;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Repositories
{
    public class InMemoryTestExplanationRepository : IExplanationRepository
    {
        private readonly List<Explanation> explanations;

        public InMemoryTestExplanationRepository() => this.explanations = new();

        public Explanation? TryGetByTarget(string target) =>
            this.explanations.SingleOrDefault(x => x.Target.Value == target);

        public void Feed(Explanation explanation) => this.explanations.Add(explanation);
    }
}