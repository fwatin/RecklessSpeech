using FluentAssertions;
using RecklessSpeech.AcceptanceTests.Configuration;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Infrastructure.Sequences.Repositories;
using RecklessSpeech.Shared.Tests.Sequences;
using RecklessSpeech.Web.ViewModels.Sequences;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Features.Sequences
{
    [Binding]
    [Scope(Feature = "Get all sequences")]
    public class GetAllSequencesSteps : StepsBase
    {
        private readonly IDataContext dbContext;
        private readonly SequenceBuilder sequenceBuilder;

        public GetAllSequencesSteps(ScenarioContext context) : base(context)
        {
            this.sequenceBuilder = SequenceBuilder.Create(Guid.Parse("4AAB1D8C-93A4-4B27-B801-95F4F10F8393"));
            this.dbContext = this.GetService<IDataContext>();
        }

        public IReadOnlyCollection<SequenceSummaryPresentation> SequenceListResponse { get; set; } = default!;

        [Given(@"an existing sequence")]
        public void GivenAnExistingSequence() => this.dbContext.Sequences.Add(this.sequenceBuilder.BuildEntity());

        [When(@"the user retrieves sequences")]
        public async Task WhenTheUserRetrievesSequences() =>
            this.SequenceListResponse = await this.Client.Latest().SequenceRequests().GetAll();


        [Then(@"the existing sequence is returned")]
        public void ThenTheExistingSequenceIsReturned()
        {
            SequenceSummaryPresentation expected = this.sequenceBuilder.BuildSummaryPresentation();
            this.SequenceListResponse.Should().ContainEquivalentOf(expected);
        }
    }
}