using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch
{
    public interface IChatGptGateway
    {
        Explanation GetExplanation(string word);
    }
}