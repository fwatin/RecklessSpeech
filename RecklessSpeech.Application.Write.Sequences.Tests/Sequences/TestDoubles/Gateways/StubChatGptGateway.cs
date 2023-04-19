using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Gateways
{
    public class StubChatGptGateway : IChatGptGateway
    {
        private Explanation? item;

        public void Feed(Explanation explanation) => this.item = explanation;

        public Task<Explanation> GetExplanation(string word, string sentence)
        {
            return Task.FromResult(this.item!);
        }
    }
}