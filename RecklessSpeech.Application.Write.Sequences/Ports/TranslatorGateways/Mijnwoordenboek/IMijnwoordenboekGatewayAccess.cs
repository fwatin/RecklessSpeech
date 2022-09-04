namespace RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;

public interface IMijnwoordenboekGatewayAccess
{
    string GetDataForAWord(string word);
}