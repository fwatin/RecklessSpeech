using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;

namespace RecklessSpeech.Infrastructure.Sequences.TranslatorGateways.Mijnwoordenboek;

public class MijnwoordenboekGatewayLocalAccess : IMijnwoordenboekGatewayAccess
{
    public string GetDataForAWord(string word)
    {
        //C:\Users\felix\source\repos\MyProjects\RecklessSpeech\RecklessSpeech.Infrastructure.Read.Tests\Translations\Mijnwoordenboek\Data

        string parent = Directory.GetCurrentDirectory();
        parent = Directory.GetParent(parent)!.FullName;
        parent = Directory.GetParent(parent)!.FullName;
        parent = Directory.GetParent(parent)!.FullName;
        parent = Directory.GetParent(parent)!.FullName;

        string fileName = $"mijnwoordenboek_translations_for_{word}.htm";
        string localUrl = Path.Combine(parent,
            @"RecklessSpeech.Infrastructure.Read.Tests\Translations\Mijnwoordenboek\Data\", fileName);

        return File.ReadAllText(localUrl);
    }
}