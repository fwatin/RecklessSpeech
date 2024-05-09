using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch
{
    public interface ITranslatorGatewayFactory
    {
        public ITranslatorGateway GetTranslatorGateway(Language language);
    }
    public interface ITranslatorGateway
    {
        Explanation GetExplanation(string word);
    }
}