using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles;

public class DummyDictionaryGateway : IDutchTranslatorGateway
{
    public Explanation GetExplanation(string word)
    {
        throw new NotImplementedException();
    }
}