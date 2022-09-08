using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;

public interface ITranslatorGateway
{
    Explanation GetExplanation(string word);
}