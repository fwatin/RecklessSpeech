using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Enrich;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.Enrich;

public class CaseOfEnrichSuccessful
{
    private readonly EnrichDutchSequenceCommandHandler sut;
    private readonly SequenceBuilder sequenceBuilder;
    private readonly InMemorySequencesDbContext dbContext;

    public CaseOfEnrichSuccessful()
    {
        this.dbContext = new();
        
        InMemorySequenceRepository sequenceRepository = new(this.dbContext);
        InMemoryExplanationRepository explanationRepository = new(this.dbContext);
        
        this.sut = new(
            sequenceRepository,
            explanationRepository,
            new MijnwoordenboekGateway(new MijnwoordenboekGatewayLocalAccess()));


        ExplanationBuilder explanationBuilder = ExplanationBuilder.Create(Guid.Parse("F189810B-B15E-4360-911C-5FBCCA771887"));
        this.dbContext.Explanations.Add(explanationBuilder.BuildEntity());
        
        this.sequenceBuilder = SequenceBuilder.Create(Guid.Parse("5CFF7781-7892-4172-9656-8EF0E6A76D2C"))with
        {
            Word = new WordBuilder("brood"),
            Explanation = explanationBuilder
        };
    }

    [Fact]
    public async Task Should_enrich_a_sequence_with_explanation()
    {
        //Arrange
        this.dbContext.Sequences.Add(this.sequenceBuilder.BuildEntity());
        EnrichDutchSequenceCommand command = this.sequenceBuilder.BuildEnrichCommand();

        //Act
        IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command, CancellationToken.None);

        //Assert
        events.Should().HaveCount(2);
        ExplanationAssignedToSequenceEvent assignExplanationToSequenceEvent =
            (ExplanationAssignedToSequenceEvent) events.First(x => x is ExplanationAssignedToSequenceEvent);

        ExplanationAddedEvent addExplanationEvent = (ExplanationAddedEvent) events.First(x => x is ExplanationAddedEvent);

        addExplanationEvent.Explanation.Content.Value.Should().Contain("pain");
        assignExplanationToSequenceEvent.ExplanationId.Value.Should().NotBeEmpty();
        assignExplanationToSequenceEvent.ExplanationId.Value.Should().Be(addExplanationEvent.Explanation.ExplanationId.Value);
    }
}