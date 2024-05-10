using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Gateways
{
    public class StubChatGptGateway : IChatGptGateway
    {
        private Explanation? item;

        public void Feed(Explanation explanation) => this.item = explanation;

        public Task<Explanation> GetExplanation(Sequence sequence)
        {
            return Task.FromResult(this.item!);
        }

        public Task<string> GetSingleWordTranslation(WordSequence wordSequence, Explanation explanationWithChatGpt)
        {
            return Task.FromResult("");
        }
    }
}