using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles;

public class StubDictionaryGateway : IDutchTranslatorGateway
{
    private ExplanationBuilder builder;

    public void Feed(ExplanationBuilder builder)
    {
        this.builder = builder;
    }
    public Explanation GetExplanation(string word)
    {
        return builder.BuildDomain();
    }
}