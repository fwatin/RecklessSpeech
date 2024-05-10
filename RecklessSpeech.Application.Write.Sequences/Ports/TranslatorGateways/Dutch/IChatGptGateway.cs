using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch
{
    public interface IChatGptGateway
    {
        Task<Explanation> GetExplanation(Sequence sequence);
        Task<string> GetSingleWordTranslation(WordSequence wordSequence, Explanation explanationWithChatGpt);
    }
}