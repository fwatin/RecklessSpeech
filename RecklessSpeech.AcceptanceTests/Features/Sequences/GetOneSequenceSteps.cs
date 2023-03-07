using FluentAssertions;
using RecklessSpeech.AcceptanceTests.Configuration;
using RecklessSpeech.Shared.Tests.Sequences;
using RecklessSpeech.Web.ViewModels.Sequences;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Features.Sequences
{
    [Binding]
    [Scope(Feature = "Get one sequence")]
    public class GetOneSequenceSteps : StepsBase
    {
        private readonly SequenceBuilder sequenceBuilder;

        public GetOneSequenceSteps(ScenarioContext context) : base(context) =>
            this.sequenceBuilder = SequenceBuilder.Create(Guid.Parse("4AAB1D8C-93A4-4B27-B801-95F4F10F8393"));

        public SequenceSummaryPresentation? SequenceResponse { get; set; }

        [Given(@"an existing sequence")]
        public void GivenAnExistingSequence() => this.DbContext.Sequences.Add(this.sequenceBuilder.BuildEntity());

        [When(@"the user retrieves this sequence")]
        public async Task WhenTheUserRetrievesSequences() => this.SequenceResponse =
            await this.Client.Latest().SequenceRequests().GetOne(this.sequenceBuilder.SequenceId.Value);

        [When(@"the user tries to get an unknown sequence")]
        public async Task WhenTheUserTriesToGetAnUnknownSequence()
            => await this.Client.Latest().SequenceRequests().GetOne(Guid.Parse("A44DFAD1-3742-42E1-A56D-D4E68AC65DFC"));


        [Then(@"the existing sequence is returned")]
        public void ThenTheExistingSequenceIsReturned()
        {
            SequenceSummaryPresentation expected = this.sequenceBuilder.BuildSummaryPresentation();
            this.SequenceResponse.Should().BeEquivalentTo(expected);
        }
    }
}