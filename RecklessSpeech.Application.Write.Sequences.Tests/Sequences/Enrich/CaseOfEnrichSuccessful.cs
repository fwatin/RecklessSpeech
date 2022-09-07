using FluentAssertions;
using RecklessSpeech.Application.Write.Sequences.Commands;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Domain.Shared;
using RecklessSpeech.Infrastructure.Databases;
using RecklessSpeech.Infrastructure.Read;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Infrastructure.Sequences.TranslatorGateways.Mijnwoordenboek;
using RecklessSpeech.Shared.Tests.Sequences;
using Xunit;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.Enrich;

public class CaseOfEnrichSuccessful
{
    private readonly EnrichSequenceCommandHandler sut;
    private readonly SequenceBuilder builder;
    private readonly InMemorySequenceQueryRepository sequenceRepository;
    private InMemorySequencesDbContext dbContext;

    public CaseOfEnrichSuccessful()
    {
        this.dbContext = new();
        this.sequenceRepository = new InMemorySequenceQueryRepository(this.dbContext);
        this.sut = new(this.sequenceRepository, new MijnwoordenboekGateway(new MijnwoordenboekGatewayLocalAccess()));
        this.builder = SequenceBuilder.Create(Guid.Parse("5CFF7781-7892-4172-9656-8EF0E6A76D2C"))with
        {
            Word = new WordBuilder("brood")
        };
    }

    [Fact]
    public async Task Should_add_a_new_sequence()
    {
        //Arrange
        this.dbContext.Sequences.Add(this.builder.BuildEntity());
        EnrichSequenceCommand command = this.builder.BuildEnrichCommand();

        //Act
        IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command, CancellationToken.None);

        //Assert
        events.Should().HaveCount(1);
        ((events.First() as EnrichSequenceEvent)!).Explanation.Value.Should().Contain("pain");
    }
}