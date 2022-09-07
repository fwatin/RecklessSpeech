using FluentAssertions;
using RecklessSpeech.Application.Write.Sequences.Commands;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Shared;
using RecklessSpeech.Infrastructure.Databases;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Infrastructure.Sequences.TranslatorGateways.Mijnwoordenboek;
using RecklessSpeech.Shared.Tests.Explanations;
using RecklessSpeech.Shared.Tests.Sequences;
using Xunit;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.Enrich;

public class CaseOfEnrichSuccessful
{
    private readonly EnrichSequenceCommandHandler sut;
    private readonly SequenceBuilder sequenceBuilder;
    private readonly InMemorySequenceRepository sequenceRepository;
    private InMemorySequencesDbContext dbContext;
    private ExplanationBuilder explanationBuilder;

    public CaseOfEnrichSuccessful()
    {
        this.dbContext = new();
        this.sequenceRepository = new InMemorySequenceRepository(this.dbContext);
        this.sut = new(this.sequenceRepository, new MijnwoordenboekGateway(new MijnwoordenboekGatewayLocalAccess()));


        this.explanationBuilder = ExplanationBuilder.Create(Guid.Parse("F189810B-B15E-4360-911C-5FBCCA771887"));
        this.sequenceBuilder = SequenceBuilder.Create(Guid.Parse("5CFF7781-7892-4172-9656-8EF0E6A76D2C"))with
        {
            Word = new WordBuilder("brood"),
            Explanation = this.explanationBuilder
        };
    }

    [Fact]
    public async Task Should_enrich_a_sequence_with_explanation()
    {
        //Arrange
        this.dbContext.Sequences.Add(this.sequenceBuilder.BuildEntity());
        EnrichSequenceCommand command = this.sequenceBuilder.BuildEnrichCommand();

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