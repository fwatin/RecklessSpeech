using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Gateways
{
    public class StubTranslatorGatewayFactory : ITranslatorGatewayFactory
    {
        private readonly StubTranslatorGateway stub = new();
        public StubTranslatorGateway GetStub => this.stub;
        public ITranslatorGateway GetTranslatorGateway(Language language) => this.stub;
    }
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