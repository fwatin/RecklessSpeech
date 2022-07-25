using FluentAssertions;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Shared.Tests;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Features.Sequences;

[Binding]
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
        this.importFileContent = Some.SomeSequenceContent;
    }

    [When(@"the users imports this file")]
    public async Task WhenTheUsersImportsThisFile()
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
}