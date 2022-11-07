using FluentAssertions;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;
using RecklessSpeech.Shared.Tests.Sequences;
using Xunit;

namespace RecklessSpeech.Infrastructure.Sequences.Tests.DomainEvents;

public class CaseOfSequenceEvents : BaseInfrastructureTests
{
    [Fact]
    public async Task ShouldSaveNewSequence()
    {
        //Arrange
        SequenceBuilder sequenceBuilder = SequenceBuilder.Create(Guid.Parse("0CE0088F-256B-483A-9174-CAA40A558B05")) with
        {
            HtmlContent = new HtmlContentBuilder("this is a html content")
        };
        SequenceEntity expectedEntity = sequenceBuilder.BuildEntity();

        //Act
        await this.Sut.ApplyEvents(new List<DomainEventIdentifier>()
            {new(Guid.Parse("6328FAC7-7AC9-4F3F-8652-9161FF345D4E"), sequenceBuilder.BuildEvent())});

        //Assert
        this.InMemorySequencesDbContext.Sequences.Should().ContainEquivalentOf(expectedEntity);
    }

    [Fact]
    public async Task ShouldUpdateDictionaryInASequence()
    {
        //Arrange
        Guid sequenceId = Guid.Parse("618D7FF9-DB61-4949-ABE8-A8ABDC0B2221");
        Guid dictionaryId = Guid.Parse("7B52D64A-81D0-46C3-B17B-EC4B6FF96143");
        
        SequenceBuilder sequenceBuilderWithDictionary = SequenceBuilder.Create(sequenceId) with
        {
            LanguageDictionaryId = new(dictionaryId)
        };
        
        SequenceBuilder sequenceBuilderWithoutDictionary = SequenceBuilder.Create(sequenceId) with
        {
            LanguageDictionaryId = null
        };
        this.InMemorySequencesDbContext.Sequences.Add(sequenceBuilderWithoutDictionary.BuildEntity());

        //Act
        await ApplyEvent(sequenceBuilderWithDictionary.BuildAssignLanguageDictionaryEvent());

        //Assert
        SequenceEntity expectedEntity = sequenceBuilderWithDictionary.BuildEntity();
        this.InMemorySequencesDbContext.Sequences.Should().ContainEquivalentOf(expectedEntity);
    }

    
}