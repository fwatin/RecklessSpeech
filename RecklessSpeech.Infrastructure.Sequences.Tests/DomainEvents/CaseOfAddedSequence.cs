using FluentAssertions;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;
using RecklessSpeech.Shared.Tests.Sequences;
using Xunit;

namespace RecklessSpeech.Infrastructure.Sequences.Tests.DomainEvents
{
    public class CaseOfAddedSequence : BaseInfrastructureTests
    {
        [Fact]
        public async Task ShouldSaveNewSequence()
        {
            //Arrange
            SequenceBuilder sequenceBuilder =
                SequenceBuilder.Create(Guid.Parse("0CE0088F-256B-483A-9174-CAA40A558B05")) with { Explanation = null };
            SequenceEntity expectedEntity = sequenceBuilder.BuildEntity();

            //Act
            await this.Sut.ApplyEvents(new List<DomainEventIdentifier>
            {
                new(Guid.Parse("6328FAC7-7AC9-4F3F-8652-9161FF345D4E"), sequenceBuilder.BuildEvent())
            });

            //Assert
            SequenceEntity result = this.InMemoryDataContext.Sequences.First();
            result.Should().BeEquivalentTo(expectedEntity);
        }
    }
}