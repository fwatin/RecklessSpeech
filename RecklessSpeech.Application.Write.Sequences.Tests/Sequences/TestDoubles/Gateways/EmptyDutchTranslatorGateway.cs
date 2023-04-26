using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Gateways
{
    public class EmptyDutchTranslatorGateway : IDutchTranslatorGateway
    {
        public Explanation GetExplanation(string word)
        {
            return ExplanationBuilder.Create() with
            {
                Content = new(""),
            };
        }
    }
}