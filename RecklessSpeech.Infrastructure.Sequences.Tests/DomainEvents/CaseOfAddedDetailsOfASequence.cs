using FluentAssertions;
using RecklessSpeech.Application.Core.Events;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Shared.Tests.Sequences;
using Xunit;

namespace RecklessSpeech.Infrastructure.Sequences.Tests.DomainEvents
{
    public class CaseOfAddedDetailsOfASequence : BaseInfrastructureTests
    {
        [Fact]
        public async Task ShouldAddDetails()
        {
            //Arrange
            SequenceBuilder sequenceBuilder = SequenceBuilder.Create()with { TranslatedWord = null };
            this.InMemoryDataContext.Sequences.Add(sequenceBuilder.BuildEntity());
            SetTranslatedWordEvent ev = new(new(sequenceBuilder.SequenceId.Value), new("bread"));

            //Act
            await this.Sut.ApplyEvents(new List<IDomainEvent> { ev });

            //Assert
            SequenceDao result = this.InMemoryDataContext.Sequences.First();
            result.TranslatedWord.Should().NotBeNull();
        }
    }
}