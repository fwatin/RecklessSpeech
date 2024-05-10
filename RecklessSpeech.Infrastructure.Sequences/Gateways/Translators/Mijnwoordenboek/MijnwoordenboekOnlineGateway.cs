using HtmlAgilityPack;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Infrastructure.Sequences.Gateways.Translators.WordReference;

namespace RecklessSpeech.Infrastructure.Sequences.Gateways.Translators.Mijnwoordenboek
{
    public class TranslatorGatewayFactory : ITranslatorGatewayFactory
    {
        public ITranslatorGateway GetTranslatorGateway(Language language)
        {
            if (language is RecklessSpeech.Domain.Sequences.Explanations.English) return new EnglishWordReferenceGateway();
            if (language is RecklessSpeech.Domain.Sequences.Explanations.Dutch) return new DutchMijnWoordenboekGateway();
            if (language is RecklessSpeech.Domain.Sequences.Explanations.Italian) return new ItalianWordReferenceGateway();

            return new EmptyTranslatorGateway();

        }
        
    }
    
    public class DutchMijnWoordenboekGateway : ITranslatorGateway
    {
        public Explanation GetExplanation(string word)
        {
            (string translations, string sourceUrl) = this.GetTranslationsAndSourceForAWord(word);

            Explanation explanation = Explanation.Create( translations,"", word, sourceUrl, new Dutch());

            return explanation;
        }

        private (string, string) GetTranslationsAndSourceForAWord(string word)
        {
            string url = $"https://www.mijnwoordenboek.nl/vertaal/NL/FR/{word}";

            HtmlWeb web = new();

            HtmlNode? mainNode = web.Load(url).DocumentNode;

            HtmlNode node = this.GetNodeByNameAndAttribute(mainNode, "div", "class", "slider-wrap");

            HtmlNode endNode = Remove(node);

            return (endNode.ParentNode.InnerHtml, url);
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

        private static HtmlNode Remove(HtmlNode doc)
        {
            List<string> paths = doc.Descendants("script").Select(n => n.XPath).ToList();

            foreach (HtmlNode? htmlNode in paths.Select(doc.SelectSingleNode).Where(htmlNode => htmlNode != null))
            {
                htmlNode.Remove();
            }

            return doc;
        }
    }
}