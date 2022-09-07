using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;

public interface ITranslatorGateway
{
    Explanation GetExplanation(string word);
}