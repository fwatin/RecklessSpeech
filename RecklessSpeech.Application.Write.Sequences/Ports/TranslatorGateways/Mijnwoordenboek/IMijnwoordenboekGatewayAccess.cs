namespace RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;

public interface IMijnwoordenboekGatewayAccess
{
    (string,string) GetTranslationsAndSourceForAWord(string word);
}