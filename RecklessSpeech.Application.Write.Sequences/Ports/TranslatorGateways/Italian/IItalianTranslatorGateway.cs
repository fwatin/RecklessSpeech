using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Italian
{
    public interface IItalianTranslatorGateway
    {
        Explanation GetExplanation(string word);
    }
}