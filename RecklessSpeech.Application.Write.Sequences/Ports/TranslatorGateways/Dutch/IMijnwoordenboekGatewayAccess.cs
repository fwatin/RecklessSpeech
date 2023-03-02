namespace RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;

public interface IMijnwoordenboekGatewayAccess
{
    (string,string) GetTranslationsAndSourceForAWord(string word);
}