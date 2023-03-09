using FluentAssertions;
using RecklessSpeech.Application.Core.Events;
using RecklessSpeech.Infrastructure.Entities;
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
                SequenceBuilder.Create(Guid.Parse("0CE0088F-256B-483A-9174-CAA40A558B05"));
            SequenceDao expectedDao = sequenceBuilder.BuildEntity();

            //Act
            await this.Sut.ApplyEvents(new List<IDomainEvent> { sequenceBuilder.BuildEvent() });

            //Assert
            SequenceDao result = this.InMemoryDataContext.Sequences.First();
            result.Should().BeEquivalentTo(expectedDao);
        }
    }
}