using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;
using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Infrastructure.Sequences.Gateways.Translators.Mijnwoordenboek
{
    public class MijnwoordenboekLocalGateway : IDutchTranslatorGateway
    {
        public Explanation GetExplanation(string word)
        {
            (string translations, string sourceUrl) = this.GetTranslationsAndSourceForAWord(word);

            Explanation explanation = Explanation.Create(Guid.NewGuid(), translations, word, sourceUrl);

            return explanation;
        }

        private (string, string) GetTranslationsAndSourceForAWord(string word)
        {
            string currentDirectory = Directory.GetCurrentDirectory();

            string fileName = $"mijnwoordenboek_translations_for_{word}.htm";

            string localUrl = Path.Combine(currentDirectory, @"Data", fileName);

            return (File.ReadAllText(localUrl), localUrl);
        }
    }
}