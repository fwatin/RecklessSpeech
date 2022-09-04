namespace RecklessSpeech.Infrastructure.Read.Tests.Translations.Mijnwoordenboek;

public class MijnwoordenboekGateway : IMijnwoordenboekGateway
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