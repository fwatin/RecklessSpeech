using FluentAssertions;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Shared.Tests.Sequences;
using Xunit;

namespace RecklessSpeech.Infrastructure.Sequences.Tests.DomainEvents
{
    public class CaseOfAssignLanguageDictionaryInASequence : BaseInfrastructureTests
    {
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
            await this.ApplyEvent(sequenceBuilderWithDictionary.BuildAssignLanguageDictionaryEvent());

            //Assert
            SequenceEntity expectedEntity = sequenceBuilderWithDictionary.BuildEntity();
            this.InMemorySequencesDbContext.Sequences.Should().ContainEquivalentOf(expectedEntity);
        }
    }
}