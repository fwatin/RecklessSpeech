namespace RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.English;

public interface IWordReferenceGatewayAccess
{
    (string, string) GetTranslationsAndSourceForAWord(string word);
}