namespace RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch
{
    public interface
        IMijnwoordenboekGatewayAccess //todo à dégager devrait juste y avoir Gateway. et faire juste un seul local aps besoin de distinguer dutch ou eng
    {
        (string, string) GetTranslationsAndSourceForAWord(string word);
    }
}