using FluentAssertions;
using HtmlAgilityPack;
using RecklessSpeech.AcceptanceTests.Configuration;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Sequences.Repositories;
using RecklessSpeech.Shared.Tests.Sequences;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Features.Sequences
{
    [Binding]
    [Scope(Feature = "Import new sequences")]
    public class ImportSequencesSteps : StepsBase
    {
        private readonly string importFileName = "import.csv";
        private readonly SequenceBuilder sequenceBuilder;
        private string importFileContent = string.Empty;

        public ImportSequencesSteps(ScenarioContext context) : base(context) => this.sequenceBuilder =
            SequenceBuilder.Create(Guid.Parse("E673F36C-9FBC-421D-9AF8-4B134E49B5C1"));

        [Given(@"a file containing some sequences")]
        public void GivenAFileContainingSomeSequences() => this.importFileContent = this.sequenceBuilder.RawCsvContent;

        [When(@"the user imports this file")]
        public async Task WhenTheUserImportsThisFile() =>
            await this.Client.Latest().SequenceRequests()
                .ImportSequences(this.importFileContent, this.importFileName);

        [Then(@"some sequences are saved")]
        public void ThenSomeSequencesAreSaved()
        {
            IDataContext sequencesContext = this.GetService<IDataContext>();
            sequencesContext.Sequences.Should().HaveCount(1);
            sequencesContext.Sequences.Single().Word.Should().BeEquivalentTo(this.sequenceBuilder.BuildEntity().Word);
        }

        [Then(@"the html in HTML Content is valid")]
        public void ThenTheHtmlInHtmlContentIsValid()
        {
            IDataContext sequencesContext = this.GetService<IDataContext>();
            SequenceDao sequence = sequencesContext.Sequences.First();
            HtmlDocument doc = new();
            doc.LoadHtml(sequence.HtmlContent);
            doc.ParseErrors.Should().BeEmpty();
        }

        [Then(@"the HTML contains some nodes for title and images")]
        public void ThenTheHtmlContainsSomeNodesForTitleAndImages()
        {
            IDataContext sequencesContext = this.GetService<IDataContext>();
            SequenceDao sequence = sequencesContext.Sequences.First();
            HtmlDocument htmlDoc = new();
            htmlDoc.LoadHtml(sequence.HtmlContent);

            HtmlNode node = htmlDoc.DocumentNode.Descendants().First(n => n.HasClass("dc-title"));
            node.InnerText.Should().Be("Moneyball");

            htmlDoc.DocumentNode.Descendants().Where(n => n.HasClass("dc-images")).Should().NotBeNullOrEmpty();
        }
    }
}