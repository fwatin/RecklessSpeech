using FluentAssertions;
using RecklessSpeech.AcceptanceTests.Configuration;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Shared.Tests.LanguageDictionaries;
using RecklessSpeech.Shared.Tests.Sequences;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Features.LanguageDictionaries;

[Binding, Scope(Feature = "Select A Language Dictionary")]
public class SelectALanguageDictionarySteps : StepsBase
{
    private LanguageDictionaryBuilder? dictionaryBuilder;
    private SequenceBuilder? sequenceBuilder;

    public SelectALanguageDictionarySteps(ScenarioContext context) : base(context)
    {
    }

    [Given(@"an existing language dictionary")]
    public void GivenAnExistingLanguageDictionary()
    {
        this.dictionaryBuilder = LanguageDictionaryBuilder.Create(Guid.Parse("059EC899-3C1D-413B-8F5A-5C4853D36975"));
        this.DbContext.LanguageDictionaries.Add(this.dictionaryBuilder.BuildEntity());
    }

    [Given(@"an existing sequence")]
    public void GivenAnExistingSequence()
    {
        this.sequenceBuilder = SequenceBuilder.Create(Guid.Parse("4C9B7529-F503-4825-B1AC-0875AB9C54E8"));
        this.DbContext.Sequences.Add(this.sequenceBuilder.BuildEntity());
    }

    [When(@"the user selects a dictionary for a sequence")]
    public async Task WhenTheUserSelectsADictionaryForASequence()
    {
        await this.Client.Latest()
            .SequenceRequests()
            .AssignLanguageDictionary(this.sequenceBuilder!.SequenceId.Value, this.dictionaryBuilder!.LanguageDictionaryId.Value);
    }

    [Then(@"the dictionary is saved for this sequence")]
    public void ThenTheDictionaryIsSavedForThisSequence()
    {
        SequenceEntity sequenceEntity = this.DbContext.Sequences.Single(x => x.Id == this.sequenceBuilder!.SequenceId.Value);
        sequenceEntity.LanguageDictionaryId.Should().Be(this.dictionaryBuilder!.LanguageDictionaryId.Value);
    }
}