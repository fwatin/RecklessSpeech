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
        private readonly InMemorySequenceQueryRepository sut;

        public CaseOfGetAll()
        {
            this.memorySequencesDbContext = new();
            this.sut = new InMemorySequenceQueryRepository(this.memorySequencesDbContext);
        }

        [Fact]
        public async Task Should_find_a_sequence()
        {
            //Arrange
            SequenceBuilder builder = SequenceBuilder.Create(Guid.Parse("46AA5502-39A3-4E17-BFF7-ECAAEF56237B"));
            this.memorySequencesDbContext.Sequences.Add(builder.BuildEntity());
            
            //Act
            IReadOnlyCollection<SequenceSummaryQueryModel> result = await this.sut.GetAll();
            
            //Assert
            SequenceSummaryQueryModel expected = builder.BuildQueryModel();
            result.Should().ContainEquivalentOf(expected);
        }
    }
}