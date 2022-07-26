using FluentAssertions;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Shared.Tests.Sequences;
using RecklessSpeech.Web.ViewModels.Sequences;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Features.Sequences;

[Binding]
public class GetAllSequencesSteps : StepsBase
{
    private readonly SequenceBuilder sequenceBuilder;
    private readonly ISequencesDbContext dbContext;
    public IReadOnlyCollection<SequenceSummaryPresentation> SequenceListResponse { get; set; }

    public GetAllSequencesSteps(ScenarioContext context) : base(context)
    {
        this.sequenceBuilder = SequenceBuilder.Create();
        this.dbContext = this.GetService<ISequencesDbContext>();
    }

    [Given(@"an existing sequence")]
    public void GivenAnExistingSequence()
    {
        this.dbContext.Sequences.Add(sequenceBuilder.BuildEntity());
    }

    [When(@"the user retrieves sequences")]
    public async Task WhenTheUserRetrievesSequences()
    {            
        this.SequenceListResponse = (await this.Client.Latest().SequenceRequests().GetAll());
    }


    [Then(@"the existing sequence is returned")]
    public void ThenTheExistingSequenceIsReturned()
    {
        this.SequenceListResponse.Should().ContainEquivalentOf(sequenceBuilder.BuildSummaryPresentation());
    }
}