using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;

namespace RecklessSpeech.Infrastructure.Sequences.TranslatorGateways.Mijnwoordenboek;

//todo à bouger dans les tests
public class MijnwoordenboekGatewayLocalAccess : IMijnwoordenboekGatewayAccess
{
    public (string, string) GetTranslationsAndSourceForAWord(string word)
    {
        string currentDirectory = Directory.GetCurrentDirectory();

        string fileName = $"mijnwoordenboek_translations_for_{word}.htm";

        string localUrl = Path.Combine(currentDirectory, @"Data", fileName);

        return (File.ReadAllText(localUrl), localUrl);
    }
}