using HtmlAgilityPack;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Italian;
using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Infrastructure.Sequences.Gateways.Translators.WordReference
{
    public class ItalianWordReferenceGateway : IItalianTranslatorGateway
    {
        public Explanation GetExplanation(string word)
        {
            (string translations, string sourceUrl) = this.GetTranslationsAndSourceForAWord(word);

            Explanation explanation = Explanation.Create(Guid.NewGuid(), translations, word, sourceUrl);

            return explanation;
        }

        private (string, string) GetTranslationsAndSourceForAWord(string word)
        {
            string url = $"https://www.wordreference.com/iten/{word}";

            HtmlWeb web = new();

            HtmlNode? mainNode = web.Load(url).DocumentNode;

            HtmlNode node = this.GetNodeByNameAndAttribute(mainNode, "div", "id", "articleWRD");

            string content = this.CleanFromSyntaxExplanations(node.InnerHtml);

            return (content, url);
        }
        
        private HtmlNode GetNodeByNameAndAttribute(HtmlNode htmlNode, string name, string attribute, string value)
        {
            List<HtmlNode> allWithName = htmlNode.Descendants(name).ToList();
            List<HtmlNode> l = new();

            foreach (HtmlNode? div in allWithName)
            {
                foreach (HtmlAttribute? att in div.Attributes)
                {
                    if (att.Name == attribute)
                    {
                        if (string.IsNullOrEmpty(value) || value.Equals(att.Value))
                        {
                            l.Add(div);
                        }
                    }
                }
            }

            return l.Last();
        }

        private string CleanFromSyntaxExplanations(string content)
        {
            List<string> toRemove = new();

            toRemove.Add(": Refers to person, place, thing, quality, etc.");
            toRemove.Add(": Verb taking a direct object--for example, \"<b>Say</b> something.\" \"She <b>found</b> the cat.\"");
            toRemove.Add(": Verb not taking a direct object--for example, \"She <b>jokes</b>.\" \"He <b>has arrived</b>.\"");
            toRemove.Add(": Describes a noun or pronoun--for example, \"a <b>tall</b> girl,\" \"an <b>interesting</b> book,\" \"a <b>big</b> house.\"");
            toRemove.Add(": Phrase with special meaning functioning as verb--for example, \"put their heads together,\" \"come to an end.\"");
            toRemove.Add(": Describes a verb, adjective, adverb, or clause--for example, \"come <b>quickly</b>,\" \"<b>very</b> rare,\"  \"happening <b>now</b>,\" \"fall <b>down</b>.\"");
            toRemove.Add(": Verb with adverb(s) or preposition(s), having special meaning, divisible--for example, \"call off\" [=cancel], \"<b>call</b> the game <b>off</b>,\" \"<b>call off</b> the game.\"");
            toRemove.Add(": Verb with adverb(s) or preposition(s), having special meaning, not divisible--for example,\"go with\" [=combine nicely]: \"Those red shoes don't <b>go with</b> my dress.\" NOT [S]\"Those red shoes don't go my dress with.\"[/S]");
            toRemove.Add(": Verb with adverb(s) or preposition(s), having special meaning and not taking direct object--for example, \"make up\" [=reconcile]: \"After they fought, they <b>made up</b>.\"");

            foreach (string s in toRemove)
            {
                if (content.Contains(s))
                {
                    content = content.Replace(s, "");
                }
            }

            return content;
        }
    }
}