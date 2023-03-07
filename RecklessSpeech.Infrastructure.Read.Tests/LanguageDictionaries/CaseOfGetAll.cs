using FluentAssertions;
using RecklessSpeech.Application.Read.Queries.LanguageDictionaries.GetAll;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Shared.Tests.LanguageDictionaries;
using Xunit;

namespace RecklessSpeech.Infrastructure.Read.Tests.LanguageDictionaries
{
    public class CaseOfGetAll
    {
        private readonly InMemoryDataContext memoryDataContext;
        private readonly InMemoryLanguageDictionaryQueryRepository sut;

        public CaseOfGetAll()
        {
            this.memoryDataContext = new();
            this.sut = new(this.memoryDataContext);
        }

        [Fact]
        public async Task Should_find_a_dictionary()
        {
            //Arrange
            LanguageDictionaryBuilder builder =
                LanguageDictionaryBuilder.Create(Guid.Parse("D5DE38E6-2E3A-4ECC-98AC-0728C64AA621"));
            this.memoryDataContext.LanguageDictionaries.Add(builder.BuildEntity());

            //Act
            IReadOnlyCollection<LanguageDictionarySummaryQueryModel> result = await this.sut.GetAll();

            //Assert
            LanguageDictionarySummaryQueryModel expected = builder.BuildQueryModel();
            result.Should().ContainEquivalentOf(expected);
        }
    }
}