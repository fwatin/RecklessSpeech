using FluentAssertions;
using RecklessSpeech.Application.Write.Sequences.Commands;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Shared;
using RecklessSpeech.Infrastructure.Databases;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Infrastructure.Sequences.TranslatorGateways.Mijnwoordenboek;
using RecklessSpeech.Shared.Tests.Explanations;
using RecklessSpeech.Shared.Tests.Sequences;
using Xunit;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.Enrich;

public class CaseOfAlreadyExistingExplanation
{
    private readonly EnrichDutchSequenceCommandHandler sut;
    private readonly SequenceBuilder sequenceBuilder;
    private readonly InMemorySequenceRepository sequenceRepository;
    private readonly InMemorySequencesDbContext dbContext;
    private readonly ExplanationBuilder explanationBuilder;
    private readonly InMemoryExplanationRepository explanationRepository;

    public CaseOfAlreadyExistingExplanation()
    {
        this.dbContext = new();
        this.sequenceRepository = new InMemorySequenceRepository(this.dbContext);
        this.explanationRepository = new InMemoryExplanationRepository(this.dbContext);

        this.sut = new(
            this.sequenceRepository,
            this.explanationRepository,
            new MijnwoordenboekGateway(new MijnwoordenboekGatewayLocalAccess()));

        this.explanationBuilder = ExplanationBuilder.Create(Guid.Parse("AE97DE7B-CDDC-47DA-822F-D832C85D150B")) with
        {
            Target = new("house")
        };

        this.sequenceBuilder = SequenceBuilder.Create(Guid.Parse("43F23F72-6302-4E8B-BF2A-72A5474AA3C3")) with
        {
            Word = new("house")
        };
    }

    [Fact]
    public async Task Should_Not_Add_Explanation_If_Already_exists()
    {
        //Arrange
        ExplanationEntity entity = this.explanationBuilder.BuildEntity();
        this.dbContext.Explanations.Add(entity);
        this.dbContext.Sequences.Add(this.sequenceBuilder.BuildEntity());
        EnrichDutchSequenceCommand command = this.sequenceBuilder.BuildEnrichCommand();

        //Act
        IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command, CancellationToken.None);

        //Assert
        events.Count.Should().Be(1);
        events.First().Should().BeOfType<ExplanationAssignedToSequenceEvent>();
    }
}