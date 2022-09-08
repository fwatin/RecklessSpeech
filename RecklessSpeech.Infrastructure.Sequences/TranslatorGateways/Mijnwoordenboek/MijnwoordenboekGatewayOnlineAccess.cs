using HtmlAgilityPack;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;

namespace RecklessSpeech.Infrastructure.Sequences.TranslatorGateways.Mijnwoordenboek;

public class MijnwoordenboekGatewayOnlineAccess : IMijnwoordenboekGatewayAccess
{
    public string GetDataForAWord(string word)
    {
        string url = $"https://www.mijnwoordenboek.nl/vertaal/NL/FR/{word}"; //todo config dans appsettings

        HtmlWeb web = new();

        HtmlNode? mainNode = web.Load(url).DocumentNode;

        HtmlNode node = GetNodeByNameAndAttribute(mainNode, "div", "class", "slider-wrap");

        HtmlNode endNode = Remove(node, "script");

        return endNode.ParentNode.InnerHtml;
    }

    private HtmlNode GetNodeByNameAndAttribute(HtmlNode htmlNode, string name, string attribute, string value)
    {
        List<HtmlNode> allWithName = htmlNode.Descendants(name).ToList();
        List<HtmlNode> l = (from div in allWithName
            from att in div.Attributes
            where att.Name == attribute
            where string.IsNullOrEmpty(value) || value.Equals(att.Value)
            select div).ToList();

        return l.Last();
    }

    private static HtmlNode Remove(HtmlNode doc, string name)
    {

        List<string> paths = doc.Descendants("script").Select(n => n.XPath).ToList();

        foreach (HtmlNode? htmlNode in paths.Select(doc.SelectSingleNode).Where(htmlNode => htmlNode != null))
        {
            htmlNode.Remove();
        }

        return doc;
    }
}