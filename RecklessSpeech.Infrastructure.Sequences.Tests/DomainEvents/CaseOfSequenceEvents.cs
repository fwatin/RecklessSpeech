using FluentAssertions;
using RecklessSpeech.Infrastructure.Databases;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;
using RecklessSpeech.Shared.Tests.Sequences;
using Xunit;

namespace RecklessSpeech.Infrastructure.Sequences.Tests.DomainEvents;

public class CaseOfSequenceEvents
{
    private readonly SequenceBuilder sequenceBuilder;
    private readonly InMemorySequencesDbContext inMemorySequencesDbContext;
    private readonly DomainEventsRepository sut;

    public CaseOfSequenceEvents()
    {
        this.sequenceBuilder = SequenceBuilder.Create(Guid.Parse("0CE0088F-256B-483A-9174-CAA40A558B05"));
        this.inMemorySequencesDbContext = new();
        this.sut = new DomainEventsRepository(new IDomainEventRepository[]
        {
            new SequenceDomainEventRepository(this.inMemorySequencesDbContext)
        });
    }

    [Fact]
    public async Task ShouldSaveSequence()
    {
        //Arrange
        SequenceEntity expectedEntity = sequenceBuilder.BuildEntity();

        //Act
        await this.sut.ApplyEvents(new List<DomainEventIdentifier>()
            {new(Guid.Parse("6328FAC7-7AC9-4F3F-8652-9161FF345D4E"), sequenceBuilder.BuildEvent())});

        //Assert
        this.inMemorySequencesDbContext.Sequences.Should().ContainEquivalentOf(expectedEntity);
    }
}