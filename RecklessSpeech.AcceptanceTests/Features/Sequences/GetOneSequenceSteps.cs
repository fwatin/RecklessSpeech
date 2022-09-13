using FluentAssertions;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Shared.Tests.Sequences;
using RecklessSpeech.Web.ViewModels.Sequences;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Features.Sequences;

[Binding, Scope(Feature = "Get one sequence")]

public class GetOneSequenceSteps : StepsBase
{
    private readonly SequenceBuilder sequenceBuilder;
    private readonly ISequencesDbContext dbContext;
    public SequenceSummaryPresentation? SequenceResponse { get; set; } = default!;

    public GetOneSequenceSteps(ScenarioContext context) : base(context)
    {
        this.sequenceBuilder = SequenceBuilder.Create(Guid.Parse("4AAB1D8C-93A4-4B27-B801-95F4F10F8393"));
        this.dbContext = GetService<ISequencesDbContext>();
    }

    [Given(@"an existing sequence")]
    public void GivenAnExistingSequence()
    {
        this.dbContext.Sequences.Add(this.sequenceBuilder.BuildEntity());
    }

    [When(@"the user retrieves this sequence")]
    public async Task WhenTheUserRetrievesSequences()
    {            
        this.SequenceResponse = (await this.Client.Latest().SequenceRequests().GetOne(this.sequenceBuilder.SequenceId.Value));
    }


    [Then(@"the existing sequence is returned")]
    public void ThenTheExistingSequenceIsReturned()
    {
        this.SequenceResponse.Should().BeEquivalentTo(this.sequenceBuilder.BuildSummaryPresentation());
    }
}