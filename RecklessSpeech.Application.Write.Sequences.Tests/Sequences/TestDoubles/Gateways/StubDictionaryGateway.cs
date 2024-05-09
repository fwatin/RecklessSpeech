using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Gateways
{
    public class StubTranslatorGateway : ITranslatorGateway
    {
        private Explanation? item;

        public void Feed(Explanation explanation) => this.item = explanation;

        public Explanation GetExplanation(string word)
        {
            return this.item!;
        }
    }
}