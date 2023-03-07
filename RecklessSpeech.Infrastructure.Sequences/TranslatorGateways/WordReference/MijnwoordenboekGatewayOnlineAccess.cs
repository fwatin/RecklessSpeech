using HtmlAgilityPack;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.English;

namespace RecklessSpeech.Infrastructure.Sequences.TranslatorGateways.WordReference
{
    public class WordReferenceGatewayOnlineAccess : IWordReferenceGatewayAccess
    {
        public (string, string) GetTranslationsAndSourceForAWord(string word)
        {
            string url = $"https://www.wordreference.com/enfr/{word}"; //todo config dans appsettings

            HtmlWeb web = new();

            HtmlNode? mainNode = web.Load(url).DocumentNode;

            HtmlNode node = this.GetNodeByNameAndAttribute(mainNode, "div", "id", "articleWRD");

            string content = this.CleanFromSyntaxExplanations(node.InnerHtml);

            return (content, url);
        }

        private HtmlNode GetNodeByNameAndAttribute(HtmlNode htmlNode, string name, string attribute, string value)
        {
            List<HtmlNode> allWithName = htmlNode.Descendants(name).ToList();
            List<HtmlNode> l = new List<HtmlNode>();

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
            List<string> toRemove = new List<string>();

            toRemove.Add(": Refers to person, place, thing, quality, etc.");
            toRemove.Add(
                ": s'utilise avec les articles <b>\"le\", \"l'\" </b>(devant une voyelle ou un h muet), <b>\"un\"</b>. <i>Ex : gar�on - nm &gt; On dira \"<b>le</b> gar�on\" ou \"<b>un</b> gar�on\". </i>");
            toRemove.Add(
                ": s'utilise avec les articles <b>\"le\", \"l'\" </b>(devant une voyelle ou un h muet), <b>\"un\"</b>. <i>Ex : gar�on - nm > On dira \"<b>le</b> gar�on\" ou \"<b>un</b> gar�on\". </i>");
            toRemove.Add(
                ": Verb taking a direct object--for example, \"<b>Say</b> something.\" \"She <b>found</b> the cat.\"");
            toRemove.Add(
                ": verbe qui s'utilise avec un compl�ment d'objet direct (COD). <i>Ex : \"J'<b>�cris</b> une lettre\". \"Elle <b>a retrouv�</b> son chat\".</i>");
            toRemove.Add(
                ": Verb not taking a direct object--for example, \"She <b>jokes</b>.\" \"He <b>has arrived</b>.\"");
            toRemove.Add(
                ": verbe qui s'utilise sans compl�ment d'objet direct (COD). <i>Ex : \"Il est <b>parti</b>.\" \"Elle <b>a ri</b>.\"</i>");
            toRemove.Add(
                ": verbe qui s'utilise avec le pronom r�fl�chi \"se\", qui s'accorde avec le sujet. <i>Ex : <b>se regarder</b> : \"Je <b>me</b> regarde dans le miroir. Tu <b>te</b> regardes dans le miroir.\"</i>. Les verbes pronominaux se conjuguent toujours avec l'auxiliaire \"�tre\". <i>Ex : \"Elle <b>a</b> lav� la voiture\" mais \"Elle s'<b>est</b> lav�e.\"</i>");
            toRemove.Add(
                ": modifie un nom. Il est g�n�ralement plac� apr�s le nom et s'accorde avec le nom (<i>ex : un ballon bleu, un<b>e</b> balle bleu<b>e</b></i>). En g�n�ral, seule la forme au masculin singulier est donn�e. Pour former le <b>f�minin</b>, on ajoute <b>\"e\"</b> (<i>ex : petit > petit<b>e</b></i>) et pour former le <b>pluriel</b>, on ajoute <b>\"s\"</b> (<i>ex : petit > petit<b>s</b></i>). Pour les formes qui sont \"irr�guli�res\" au f�minin, celles-ci sont donn�es (<i>ex : irr�gulier, irr�guli�re</i> > irr�gulier = forme masculine, irr�guli�re = forme f�minine)");
            toRemove.Add(
                ": Describes a noun or pronoun--for example, \"a <b>tall</b> girl,\" \"an <b>interesting</b> book,\" \"a <b>big</b> house.\"");
            toRemove.Add(
                ": s'utilise avec les articles <b>\"la\", \"l'\" </b>(devant une voyelle ou un h muet), <b>\"une\"</b>. <i>Ex : fille - nf > On dira \"<b>la</b> fille\" ou \"<b>une</b> fille\".</i> Avec un nom f�minin, l'adjectif s'accorde. En g�n�ral, on ajoute un \"e\" � l'adjectif. Par exemple, on dira \"une petit<b>e</b> fille\".");
            toRemove.Add(": groupe de mots fonctionnant comme un verbe. <i>Ex : \"faire r�f�rence �\"");
            toRemove.Add(
                ": modifie un adjectif ou un verbe. Est toujours invariable ! <i>Ex : \"Elle est <b>tr�s</b> grande.\" \"Je marche <b>lentement</b>.\"</i>");
            toRemove.Add(
                ": Phrase with special meaning functioning as verb--for example, \"put their heads together,\" \"come to an end.\"");
            toRemove.Add(
                ": Describes a verb, adjective, adverb, or clause--for example, \"come <b>quickly</b>,\" \"<b>very</b> rare,\"  \"happening <b>now</b>,\" \"fall <b>down</b>.\"");
            toRemove.Add(
                ": s'utilise avec l'article d�fini <b>\"les\"</b>. <b>nmpl</b> = nom pluriel au <b>masculin</b>, <b>nfpl</b> = nom pluriel au <b>f�minin");
            toRemove.Add(": s'utilise avec l'article d�fini <b>\"les\"</b>. <i>Ex : \"algues\"</i>");
            toRemove.Add(
                ": Verb with adverb(s) or preposition(s), having special meaning, divisible--for example, \"call off\" [=cancel], \"<b>call</b> the game <b>off</b>,\" \"<b>call off</b> the game.\"");
            toRemove.Add(
                ": <b>Adjectif invariable</b> : adjectif qui a la m�me forme au singulier et au pluriel, au masculin et au f�minin. <i>Ex : \"canon\" : un gar�on canon, une fille canon, des gar�ons canon, des filles canon.</i>");
            toRemove.Add(
                ": Verb with adverb(s) or preposition(s), having special meaning, not divisible--for example,\"go with\" [=combine nicely]: \"Those red shoes don't <b>go with</b> my dress.\" NOT [S]\"Those red shoes don't go my dress with.\"[/S]");
            toRemove.Add(
                ": nom masculin qui a la m�me forme au pluriel. <i>Ex : \"un <b>?porte-cl�s</b>, des <b>porte-cl�s</b>\"</i>");
            toRemove.Add(
                ": nom � la fois masculin et f�minin qui a la m�me forme au pluriel. <i>Ex : \"un <b>casse-pieds</b>, une <b>casse-pieds</b>, des <b>casse-pieds</b>\"</i></span></em></td></tr><tr class='odd'><td>&nbsp;</td><td class='To2'>&nbsp;<span class='dsense'>(<i>tr�s familier</i>)</span></td><td class='ToWrd'>l�che-cul <em class='tooltip POS2'>nmf inv<span><i>nom masculin et f�minin invariable</i>: nom � la fois masculin et f�minin qui a la m�me forme au pluriel. <i>Ex : \"un <b>casse-pieds</b>, une <b>casse-pieds</b>, des <b>casse-pieds</b>\"</i>");
            toRemove.Add(
                ": groupe de mots qui servent d'adverbe. Toujours invariable ! <i>Ex : \"avec souplesse\"</i>");
            toRemove.Add(
                ": Verb with adverb(s) or preposition(s), having special meaning and not taking direct object--for example, \"make up\" [=reconcile]: \"After they fought, they <b>made up</b>.\"");
            toRemove.Add(
                ": groupe de mots qui servent d'adjectif. Se place normalement apr�s le nom et reste identique au pluriel<i>Ex : \"ballon <b>de football</b>, des ballons <b>de football</b>\"</i>");

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