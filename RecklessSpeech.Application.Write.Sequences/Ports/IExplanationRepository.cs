using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Application.Write.Sequences.Ports;

public interface IExplanationRepository
{
    Explanation? TryGetByTarget(string id);
}