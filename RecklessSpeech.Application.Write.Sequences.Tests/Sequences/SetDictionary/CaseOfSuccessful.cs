using FluentAssertions;
using RecklessSpeech.Application.Write.Sequences.Commands;
using RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Repositories;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Domain.Shared;
using RecklessSpeech.Infrastructure.Databases;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Shared.Tests.Sequences;
using Xunit;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.SetDictionary;

public class CaseOfSuccessful
{
    private readonly AssignLanguageDictionaryCommandHandler sut;
    readonly Guid existingSequenceId = Guid.Parse("DAA0AAC9-8F31-46A2-AC73-5F6414B29F68");
    private readonly AssignLanguageDictionaryCommand command;
    private readonly SequenceBuilder sequenceBuilder;

    public CaseOfSuccessful()
    {
        InMemoryTestSequenceRepository sequenceRepository = new();
        this.sut = new(sequenceRepository);
        sequenceBuilder = SequenceBuilder.Create(this.existingSequenceId) with
        {
            LanguageDictionaryId = new(Guid.Parse("3E05B5FE-950B-4242-952D-0B926FF83D9B"))  
        };
        sequenceRepository.Feed(sequenceBuilder);
        command = sequenceBuilder.BuildAssignDictionaryCommand();

    }

    [Fact]
    public async Task Should_set_dictionary_on_a_sequence()
    {
        //Act
        IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command, CancellationToken.None);

        //Assert
        AssignLanguageDictionaryInASequenceEvent dictionaryInASequenceEvent =
            (AssignLanguageDictionaryInASequenceEvent) events.First(x => x is AssignLanguageDictionaryInASequenceEvent);

        dictionaryInASequenceEvent.SequenceId.Value.Should().Be(sequenceBuilder.SequenceId.Value);
        dictionaryInASequenceEvent.LanguageDictionaryId!.Value.Should().Be(sequenceBuilder.LanguageDictionaryId!.Value);
    }
}