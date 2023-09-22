namespace RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Italian
{
    public interface IWordReferenceGatewayAccess
    {
        (string, string) GetTranslationsAndSourceForAWord(string word);
    }
}