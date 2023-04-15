using FluentAssertions;
using RecklessSpeech.AcceptanceTests.Configuration;
using RecklessSpeech.Infrastructure.Sequences.Repositories;
using RecklessSpeech.Web.ViewModels.Sequences;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Features.Sequences
{
    [Binding]
    [Scope(Feature = "Import sequences from zip")]
    public class ImportSequencesFromZipSteps : StepsBase
    {
        private string? filePath;
        private IReadOnlyCollection<SequenceSummaryPresentation>? result;

        public ImportSequencesFromZipSteps(ScenarioContext context) : base(context)
        {
        }

        [Given(@"a zip file containing two sequences")]
        public void GivenAZipFileContainingTwoSequences()
        {
            //D:\Dev\MyProjects\RecklessSpeech\RecklessSpeech.AcceptanceTests\bin\Debug\net6.0\Features\Sequences\Data
            this.filePath = Path.Combine(AppContext.BaseDirectory, "Features", "Sequences", "Data",
                "lln_anki_items_2023-4-11_174427.zip");
        }

        [When(@"the user imports this zip file")]
        public async Task WhenTheUserImportsThisZipFile()
        {
            this.result = await this.Client.Latest().SequenceRequests()
                .ImportSequencesFromZip(this.filePath!);
        }

        [Then(@"two sequences are saved")]
        public void ThenTwoSequencesAreSaved()
        {
            IDataContext sequencesContext = this.GetService<IDataContext>();
            sequencesContext.Sequences.Should().HaveCount(2);
            
            this.result!.Should().HaveCount(2);
        }
    }
}