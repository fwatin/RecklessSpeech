using FluentAssertions;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Shared.Tests.Sequences;
using Xunit;

namespace RecklessSpeech.Infrastructure.Read.Tests.Sequences
{
    public class CaseOfGetAll
    {
        private readonly InMemorySequencesDbContext memorySequencesDbContext;
        private readonly SequenceQueryRepository sut;

        public CaseOfGetAll()
        {
            this.memorySequencesDbContext = new();
            this.sut = new SequenceQueryRepository(this.memorySequencesDbContext);
        }

        [Fact]
        public async Task Should_find_a_sequence()
        {
            //Arrange
            var builder = SequenceBuilder.Create();
            memorySequencesDbContext.Sequences.Add(builder.BuildEntity());
            
            //Act
            IReadOnlyCollection<SequenceSummaryQueryModel> result = await this.sut.GetAll();
            
            //Assert
            result.Should().ContainEquivalentOf(builder.BuildQueryModel());
        }
    }
}