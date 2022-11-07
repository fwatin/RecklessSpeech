using FluentAssertions;
using RecklessSpeech.Application.Read.Queries.LanguageDictionaries.GetAll;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Shared.Tests.LanguageDictionaries;
using Xunit;
namespace RecklessSpeech.Infrastructure.Read.Tests.LanguageDictionaries;

public class CaseOfGetAll
{
    private readonly InMemorySequencesDbContext memorySequencesDbContext;
    private readonly InMemoryLanguageDictionaryQueryRepository sut;

    public CaseOfGetAll()
    {
        this.memorySequencesDbContext = new();
        this.sut = new InMemoryLanguageDictionaryQueryRepository(this.memorySequencesDbContext);
    }
    
    [Fact]
    public async Task Should_find_a_dictionary()
    {
        //Arrange
        LanguageDictionaryBuilder builder = LanguageDictionaryBuilder.Create(Guid.Parse("D5DE38E6-2E3A-4ECC-98AC-0728C64AA621"));
        this.memorySequencesDbContext.LanguageDictionaries.Add(builder.BuildEntity());
            
        //Act
        IReadOnlyCollection<LanguageDictionarySummaryQueryModel> result = await this.sut.GetAll();
            
        //Assert
        LanguageDictionarySummaryQueryModel expected = builder.BuildQueryModel();
        result.Should().ContainEquivalentOf(expected);
    }
}