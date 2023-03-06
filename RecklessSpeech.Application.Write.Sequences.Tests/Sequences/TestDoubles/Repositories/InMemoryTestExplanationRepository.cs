using RecklessSpeech.Application.Write.Sequences.Ports;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Repositories;

public class InMemoryTestExplanationRepository : IExplanationRepository
{
    private readonly List<Explanation> explanations;

    public InMemoryTestExplanationRepository()
    {
        this.explanations = new();
    }

    public void Feed(Explanation explanation) => this.explanations.Add(explanation);
    public Explanation? TryGetByTarget(string target)
    {
        return explanations.SingleOrDefault(x => x.Target.Value == target);
    }
}