using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;

namespace RecklessSpeech.Infrastructure.Sequences.TranslatorGateways.Mijnwoordenboek;

public class MijnwoordenboekGatewayLocalAccess : IMijnwoordenboekGatewayAccess
{
    public (string, string) GetDataForAWord(string word)
    {
        string currentDirectory = Directory.GetCurrentDirectory();

        string fileName = $"mijnwoordenboek_translations_for_{word}.htm";

        string localUrl = Path.Combine(currentDirectory, @"Data", fileName);

        return (File.ReadAllText(localUrl), localUrl);
    }
}