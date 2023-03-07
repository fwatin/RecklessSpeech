using RecklessSpeech.Application.Write.Sequences.Ports;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Repositories
{
    public class DummyExplanationRepository : IExplanationRepository
    {
        public Explanation? TryGetByTarget(string target) => throw new NotImplementedException();
    }
}