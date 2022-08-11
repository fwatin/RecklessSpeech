﻿using FluentAssertions;
using HtmlAgilityPack;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Shared.Tests;
using RecklessSpeech.Shared.Tests.Sequences;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Features.Sequences;

[Binding, Scope(Feature = "Import new sequences")]
public class ImportSequencesSteps : StepsBase
{
    private string importFileContent = string.Empty;
    private readonly string importFileName = "import.csv";
    private readonly SequenceBuilder sequenceBuilder;

    public ImportSequencesSteps(ScenarioContext context) : base(context)
    {
        sequenceBuilder = SequenceBuilder.Create(Guid.Parse("E673F36C-9FBC-421D-9AF8-4B134E49B5C1"));
    }

    [Given(@"a file containing some sequences")]
    public void GivenAFileContainingSomeSequences()
    {
        this.importFileContent = sequenceBuilder.RawCsvContent;
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
        sequencesContext.Sequences.Single().Should().BeEquivalentTo(sequenceBuilder.BuildEntity(),AssertExtensions.IgnoreId);
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