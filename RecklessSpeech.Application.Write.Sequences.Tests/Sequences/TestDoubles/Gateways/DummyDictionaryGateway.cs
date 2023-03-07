using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Gateways;

public class DummyDictionaryGateway : IDutchTranslatorGateway
{
    public Explanation GetExplanation(string word)
    {
        throw new();
    }
}