namespace RecklessSpeech.Infrastructure.Read.Tests.Translations.Mijnwoordenboek;

public interface IMijnwoordenboekGatewayAccess
{
    string GetDataForAWord(string word);
}