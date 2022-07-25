using FluentAssertions;
using RecklessSpeech.Infrastructure.Databases;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;
using RecklessSpeech.Shared.Tests;
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
        this.sequenceBuilder = SequenceBuilder.Create();
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
        var expectedEntity = sequenceBuilder.BuildEntity();

        //Act
        await this.sut.ApplyEvents(new List<DomainEventIdentifier>() {new(Some.EventId, sequenceBuilder.BuildEvent())});

        //Assert
        this.inMemorySequencesDbContext.Sequences.Should().ContainEquivalentOf(expectedEntity);
    }
}