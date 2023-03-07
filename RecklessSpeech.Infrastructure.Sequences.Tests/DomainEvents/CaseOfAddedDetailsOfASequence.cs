using FluentAssertions;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;
using RecklessSpeech.Shared.Tests.Sequences;
using Xunit;

namespace RecklessSpeech.Infrastructure.Sequences.Tests.DomainEvents;

public class CaseOfAddedDetailsOfASequence : BaseInfrastructureTests
{
    [Fact]
    public async Task ShouldAddDetails()
    {
        //Arrange
        SequenceBuilder sequenceBuilder = SequenceBuilder.Create()with { TranslatedWord = null };
        this.InMemorySequencesDbContext.Sequences.Add(sequenceBuilder.BuildEntity());
        SetTranslatedWordEvent ev = new(new(sequenceBuilder.SequenceId.Value), new("bread"));
        
        //Act
        await this.Sut.ApplyEvents(new List<DomainEventIdentifier>()
            { new(Guid.Parse("6328FAC7-7AC9-4F3F-8652-9161FF345D4E"), ev) });

        //Assert
        SequenceEntity result = this.InMemorySequencesDbContext.Sequences.First();
        result.TranslatedWord.Should().NotBeNull();
    }
}