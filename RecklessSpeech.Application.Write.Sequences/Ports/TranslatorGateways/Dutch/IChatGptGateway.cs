using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch
{
    public interface IChatGptGateway
    {
        Task<Explanation> GetExplanation(string word, string sentence, Language language);
    }
}