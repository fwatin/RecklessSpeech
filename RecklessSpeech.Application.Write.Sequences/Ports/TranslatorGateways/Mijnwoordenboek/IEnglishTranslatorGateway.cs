using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;

public interface IEnglishTranslatorGateway
{
    Explanation GetExplanation(string word);
}