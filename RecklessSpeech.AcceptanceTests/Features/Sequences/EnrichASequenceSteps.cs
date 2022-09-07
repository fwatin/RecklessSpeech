using FluentAssertions;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Shared.Tests.Sequences;
using RecklessSpeech.Web.ViewModels.Sequences;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Features.Sequences;

[Binding, Scope(Feature = "Enrich A Sequence")]

public class EnrichASequenceSteps: StepsBase
{
    private SequenceBuilder sequenceBuilder;
    private readonly ITranslatorGateway mijnwoordenboekgateway;
    private readonly ISequencesDbContext dbContext;
    public IReadOnlyCollection<SequenceSummaryPresentation> SequenceListResponse { get; set; }

    public EnrichASequenceSteps(ScenarioContext context) : base(context)
    {
        this.dbContext = this.GetService<ISequencesDbContext>();
    }

    [Given(@"a sequence to be enriched")]
    public void GivenASequenceToBeEnriched()
    {
        this.sequenceBuilder = SequenceBuilder.Create(Guid.Parse("825B8D27-301A-4974-8024-7DE798C17765")) with
        {
            Word = new WordBuilder("brood")
        };
        this.dbContext.Sequences.Add(sequenceBuilder.BuildEntity());
    }

    [When(@"the user enriches this sequence")]
    public void WhenTheUserEnrichesThisSequence()
    {
        //call api to enrich
    }

    [Then(@"the sequence enriched data contains the raw explanation")]
    public async Task ThenTheSequenceEnrichedDataContainsTheRawExplanation()
    {
        this.SequenceListResponse = (await this.Client.Latest().SequenceRequests().GetAll());
        var sequencePresentationForBrood = SequenceListResponse.Single(x => x.Id == sequenceBuilder.SequenceId.Value);
        sequencePresentationForBrood.Explanation.Should().NotBeNullOrEmpty();
    }
}