using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.English;
using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Infrastructure.Sequences.Gateways.Translators.Mijnwoordenboek
{
    public class WordReferenceGateway : IEnglishTranslatorGateway
    {
        private readonly IWordReferenceGatewayAccess access;

        public WordReferenceGateway(IWordReferenceGatewayAccess access) => this.access = access;

        public Explanation GetExplanation(string word)
        {
            (string translations, string sourceUrl) = this.access.GetTranslationsAndSourceForAWord(word);

            Explanation explanation = Explanation.Create(Guid.NewGuid(), translations, word, sourceUrl);

            return explanation;
        }
    }
}