using FluentAssertions;
using RecklessSpeech.Application.Write.Sequences.Commands;
using RecklessSpeech.Infrastructure.Read;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Shared.Tests;
using RecklessSpeech.Shared.Tests.Notes;
using RecklessSpeech.Shared.Tests.Sequences;
using Xunit;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Notes.Send;

public class CaseOfNewNotes
{
    [Fact]
    public async Task Should_send_a_new_note_to_Anki()
    {
        //Arrange
        Guid sequenceId = Guid.Parse("79FAD304-21BC-4B58-BECF-0884016DCC11");
        string someHtml = "\"<style> some html here for this test\"";
        var sequenceBuilder = SequenceBuilder.Create(sequenceId) with
        {
            HtmlContent = new(someHtml)
        };
        var sequenceEntity = sequenceBuilder.BuildEntity();
        var dbContext = new InMemorySequencesDbContext();
        dbContext.Sequences.Add(sequenceEntity);
        var sequenceRepository = new InMemorySequenceQueryRepository(dbContext);

        var spyGateway = new SpyNoteGateway();
        var sut = new SendNotesCommandHandler(spyGateway, sequenceRepository);
        var noteBuilder = NoteBuilder.Create(sequenceId) with
        {
            QuestionBuilder = new(someHtml)
        };
        SendNotesCommand command = noteBuilder.BuildCommand();

        //Act
        await sut.Handle(command, CancellationToken.None);

        //Assert
        var expected = noteBuilder.BuildDto();
        spyGateway.Notes.Should().ContainEquivalentOf(expected);
    }
}