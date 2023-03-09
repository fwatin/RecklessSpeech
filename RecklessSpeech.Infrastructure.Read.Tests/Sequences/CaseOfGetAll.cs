using FluentAssertions;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Infrastructure.Read.Tests.TestDoubles.Repositories;
using RecklessSpeech.Shared.Tests.Sequences;
using Xunit;

namespace RecklessSpeech.Infrastructure.Read.Tests.Sequences
{
    public class CaseOfGetAll
    {
        private readonly GetAllSequencesQuery command;
        private readonly InMemoryTestSequenceQueryRepository repository;
        private readonly GetAllSequencesQueryHandler sut;

        public CaseOfGetAll()
        {
            this.repository = new();
            this.sut = new(this.repository);
            this.command = new();
        }

        [Fact]
        public async Task Should_find_a_sequence()
        {
            //Arrange
            SequenceBuilder builder = SequenceBuilder.Create(Guid.Parse("46AA5502-39A3-4E17-BFF7-ECAAEF56237B"));
            this.repository.Feed(builder.BuildQueryModel());

            //Act

            IReadOnlyCollection<SequenceSummaryQueryModel> result =
                await this.sut.Handle(this.command, CancellationToken.None);

            //Assert
            SequenceSummaryQueryModel expected = builder.BuildQueryModel();
            result.Should().ContainEquivalentOf(expected);
        }
    }
}