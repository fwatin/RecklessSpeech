using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Gateways
{
    public class EmptyTranslatorGateway : ITranslatorGateway
    {
        public Explanation GetExplanation(string word)
        {
            return ExplanationBuilder.Create() with
            {
                Content = new(""),
            };
        }

    }
    public class EmptyTranslatorGatewayFactory : ITranslatorGatewayFactory
    {
        public ITranslatorGateway GetTranslatorGateway(Language language) => new EmptyTranslatorGateway();
    }
}