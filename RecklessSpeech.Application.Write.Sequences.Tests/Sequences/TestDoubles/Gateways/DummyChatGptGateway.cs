using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Gateways
{
    public class DummyChatGptGateway : IChatGptGateway
    {
        public Task<Explanation> GetExplanation(Sequence sequence) => throw new NotImplementedException();
        public Task<string> GetSingleWordTranslation(WordSequence wordSequence, Explanation explanationWithChatGpt) => throw new NotImplementedException();
    }
}