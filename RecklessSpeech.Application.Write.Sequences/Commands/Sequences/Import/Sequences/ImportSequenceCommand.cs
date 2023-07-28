using ExCSS;
using HtmlAgilityPack;
using System.Text;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Sequences
{
    public record ImportSequenceCommand(string Word, string TranslatedSentence, string Title, long MediaId) : IRequest;

}