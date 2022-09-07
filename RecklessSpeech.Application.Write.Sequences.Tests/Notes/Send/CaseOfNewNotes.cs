using FluentAssertions;
using RecklessSpeech.Application.Write.Sequences.Commands;
using RecklessSpeech.Domain.Sequences.Notes;
using RecklessSpeech.Infrastructure.Databases;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Read;
using RecklessSpeech.Infrastructure.Sequences;
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
        SequenceBuilder sequenceBuilder = SequenceBuilder.Create(sequenceId) with
        {
            HtmlContent = new(someHtml)
        };
        SequenceEntity sequenceEntity = sequenceBuilder.BuildEntity();
        InMemorySequencesDbContext dbContext = new();
        dbContext.Sequences.Add(sequenceEntity);
        InMemorySequenceRepository sequenceRepository = new(dbContext);

        SpyNoteGateway spyGateway = new();
        SendNotesCommandHandler sut = new SendNotesCommandHandler(spyGateway, sequenceRepository);
        NoteBuilder noteBuilder = NoteBuilder.Create(sequenceId) with
        {
            Question = new(someHtml)
        };
        SendNotesCommand command = noteBuilder.BuildCommand();

        //Act
        await sut.Handle(command, CancellationToken.None);

        //Assert
        NoteDto expected = noteBuilder.BuildDto();
        spyGateway.Notes.Should().ContainEquivalentOf(expected);
    }
}