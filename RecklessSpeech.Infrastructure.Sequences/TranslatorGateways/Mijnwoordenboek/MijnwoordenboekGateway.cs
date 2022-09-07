using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Infrastructure.Sequences.TranslatorGateways.Mijnwoordenboek;

public class MijnwoordenboekGateway : ITranslatorGateway
{
    private readonly IMijnwoordenboekGatewayAccess access;

    public MijnwoordenboekGateway(IMijnwoordenboekGatewayAccess access)
    {
        this.access = access;
    }

    public Explanation GetExplanation(string word)
    {
        var data = access.GetDataForAWord(word);

        Explanation explanation = new(data);

        return explanation;
    }
}