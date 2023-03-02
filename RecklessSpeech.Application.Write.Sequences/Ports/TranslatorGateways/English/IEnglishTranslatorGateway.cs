using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.English;

public interface IEnglishTranslatorGateway
{
    Explanation GetExplanation(string word);
}