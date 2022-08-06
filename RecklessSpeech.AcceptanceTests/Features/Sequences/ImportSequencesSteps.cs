using FluentAssertions;
using HtmlAgilityPack;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Shared.Tests;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Features.Sequences;

[Binding, Scope(Feature = "Import new sequences")]
public class ImportSequencesSteps : StepsBase
{
    private string importFileContent = string.Empty;
    private readonly string importFileName = "import.csv";

    public ImportSequencesSteps(ScenarioContext context) : base(context)
    {
    }

    [Given(@"a file containing some sequences")]
    public void GivenAFileContainingSomeSequences()
    {
        this.importFileContent = Some.SomeRealCaseCsvFileContent;
    }

    [When(@"the user imports this file")]
    public async Task WhenTheUserImportsThisFile()
    {
        await this.Client.Latest().SequenceRequests()
            .ImportSequences(this.importFileContent, this.importFileName);
    }

    [Then(@"some sequences are saved")]
    public void ThenSomeSequencesAreSaved()
    {
        var sequencesContext = this.GetService<ISequencesDbContext>();
        sequencesContext.Sequences.Should().HaveCount(1);
    }

    [Then(@"the html in HTML Content is valid")]
    public void ThenTheHtmlInHtmlContentIsValid()
    {
        var sequencesContext = this.GetService<ISequencesDbContext>();
        var sequence = sequencesContext.Sequences.First();
        HtmlDocument doc = new HtmlDocument();
        doc.LoadHtml(sequence.HtmlContent);
        doc.ParseErrors.Should().BeEmpty();
    }

    [Then(@"the HTML contains some nodes for title and images")]
    public void ThenTheHtmlContainsSomeNodesForTitleAndImages()
    {
        var sequencesContext = this.GetService<ISequencesDbContext>();
        var sequence = sequencesContext.Sequences.First();
        HtmlDocument htmlDoc = new HtmlDocument();
        htmlDoc.LoadHtml(sequence.HtmlContent);
        
        HtmlNode node = htmlDoc.DocumentNode.Descendants().First(n => n.HasClass("dc-title"));
        node.InnerText.Should().Be("Moneyball");

        htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("dc-images")).Should().NotBeNullOrEmpty();
    }
}