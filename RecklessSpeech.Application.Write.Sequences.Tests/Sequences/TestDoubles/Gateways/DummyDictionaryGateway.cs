using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Gateways
{
    public class DummyDictionaryGatewayFactory : ITranslatorGatewayFactory
    {
        public ITranslatorGateway GetTranslatorGateway(Language language) => new DummyDictionaryGateway();
    }

    public class DummyDictionaryGateway : ITranslatorGateway
    {
        public Explanation GetExplanation(string word) => throw new();
    }
}