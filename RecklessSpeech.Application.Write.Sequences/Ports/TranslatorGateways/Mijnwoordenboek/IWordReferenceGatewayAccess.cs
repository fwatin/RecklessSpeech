namespace RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;

public interface IWordReferenceGatewayAccess
{
    (string, string) GetTranslationsAndSourceForAWord(string word);
}