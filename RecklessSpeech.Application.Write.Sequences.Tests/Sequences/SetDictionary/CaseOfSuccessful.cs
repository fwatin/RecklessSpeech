using ExCSS;
using FluentAssertions;
using HtmlAgilityPack;
using RecklessSpeech.Application.Write.Sequences.Commands;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Domain.Shared;
using RecklessSpeech.Infrastructure.Databases;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Shared.Tests;
using RecklessSpeech.Shared.Tests.Sequences;
using Xunit;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.SetDictionary;

public class CaseOfSuccessful
{
    private readonly AssignLanguageDictionaryCommandHandler sut;
    private readonly SequenceBuilder sequenceBuilder;
    private readonly InMemorySequencesDbContext dbContext;

    public CaseOfSuccessful()
    {
        this.dbContext = new();
        InMemorySequenceRepository sequenceRepository = new(this.dbContext);
        this.sut = new AssignLanguageDictionaryCommandHandler(sequenceRepository);
        this.sequenceBuilder = SequenceBuilder.Create(Guid.Parse("DAA0AAC9-8F31-46A2-AC73-5F6414B29F68"));
        this.dbContext.Sequences.Add(this.sequenceBuilder.BuildEntity());
    }

    [Fact]
    public async Task Should_set_dictionary_on_a_sequence()
    {
        //Arrange
        AssignLanguageDictionaryCommand command = this.sequenceBuilder.BuildAssignDictionaryCommand();

        //Act
        IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command, CancellationToken.None);

        //Assert
        AssignLanguageDictionaryInASequenceEvent dictionaryInASequenceEvent =
            (AssignLanguageDictionaryInASequenceEvent) events.First(x => x is AssignLanguageDictionaryInASequenceEvent);

        dictionaryInASequenceEvent.SequenceId.Value.Should().Be(this.sequenceBuilder.SequenceId.Value);
        dictionaryInASequenceEvent.LanguageDictionaryId.Value.Should().Be(this.sequenceBuilder.LanguageDictionaryId.Value);
    }
}