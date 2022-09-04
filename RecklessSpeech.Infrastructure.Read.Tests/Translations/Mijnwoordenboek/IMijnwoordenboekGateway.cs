namespace RecklessSpeech.Infrastructure.Read.Tests.Translations.Mijnwoordenboek;

public interface IMijnwoordenboekGateway
{
    Explanation GetExplanation(string word);
}