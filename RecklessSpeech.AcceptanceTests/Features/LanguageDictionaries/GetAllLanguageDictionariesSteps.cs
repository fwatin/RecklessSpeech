using FluentAssertions;
using RecklessSpeech.AcceptanceTests.Configuration;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Shared.Tests.LanguageDictionaries;
using RecklessSpeech.Shared.Tests.Sequences;
using RecklessSpeech.Web.ViewModels.LanguageDictionaries;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Features.LanguageDictionaries;

[Binding, Scope(Feature = "Get All Dictionaries")]
public class GetAllDictionariesSteps : StepsBase
{
    private readonly LanguageDictionaryBuilder firstLanguageDictionary;
    private readonly LanguageDictionaryBuilder secondLanguageDictionary;
    private IReadOnlyCollection<LanguageDictionarySummaryPresentation>? result;

    public GetAllDictionariesSteps(ScenarioContext context) : base(context)
    {
        this.firstLanguageDictionary = LanguageDictionaryBuilder.Create(Guid.Parse("2F21DB83-C2B2-49B9-A58C-BE0D0559C3C8"));
        this.secondLanguageDictionary = LanguageDictionaryBuilder.Create(Guid.Parse("AFECEC59-F024-46DD-9E12-CE9F1A142307"));
    }

    [Given(@"two language dictionaries")]
    public void GivenTwoLanguageDictionaries()
    {
        this.DbContext.LanguageDictionaries.Add(this.firstLanguageDictionary.BuildEntity());
        this.DbContext.LanguageDictionaries.Add(this.secondLanguageDictionary.BuildEntity());
    }
    
    [When(@"the users gets all the language dictionaries")]
    public void WhenTheUsersGetsAllTheLanguageDictionaries()
    {
    
    }
    
    [Then(@"the result contains these two")]
    public void ThenTheResultContainsTheseTwo()
    {
        this.result.Should().ContainEquivalentOf(this.firstLanguageDictionary.BuildSummaryPresentation());
        this.result.Should().ContainEquivalentOf(this.secondLanguageDictionary.BuildSummaryPresentation());
    }
}