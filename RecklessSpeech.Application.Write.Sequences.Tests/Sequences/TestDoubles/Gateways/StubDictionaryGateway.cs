using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Gateways
{
    public class StubDictionaryGateway : IDutchTranslatorGateway
    {
        private ExplanationBuilder builder;

        public Explanation GetExplanation(string word) => this.builder.BuildDomain();

        public void Feed(ExplanationBuilder builder) => this.builder = builder;
    }
}