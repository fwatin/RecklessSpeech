using FluentAssertions;
using RecklessSpeech.Application.Write.Sequences.Commands;
using RecklessSpeech.Domain.Sequences.Notes;
using RecklessSpeech.Infrastructure.Databases;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Shared.Tests.Notes;
using RecklessSpeech.Shared.Tests.Sequences;
using Xunit;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Notes.Send;

public class CaseOfNewNotes
{
    private readonly NoteBuilder noteBuilder;
    private readonly SendNotesCommandHandler sut;
    private readonly SpyNoteGateway spyGateway;
    public CaseOfNewNotes()
    {
        Guid sequenceId = Guid.Parse("79FAD304-21BC-4B58-BECF-0884016DCC11");
        const string someHtml = "\"<style> some html here for this test\"";
        SequenceBuilder sequenceBuilder = SequenceBuilder.Create(sequenceId) with
        {
            HtmlContent = new(someHtml)
        };
        SequenceEntity sequenceEntity = sequenceBuilder.BuildEntity();
        InMemorySequencesDbContext dbContext = new();
        dbContext.Sequences.Add(sequenceEntity);
        InMemorySequenceRepository sequenceRepository = new(dbContext);

        this.spyGateway = new();
        this.sut = new(this.spyGateway, sequenceRepository);
        this.noteBuilder = NoteBuilder.Create(sequenceId) with
        {
            Question = new(someHtml),
            After = new("")
        };
    }
    [Fact]
    public async Task Should_send_a_new_note_to_Anki()
    {
        //Arrange
        SendNotesCommand command = this.noteBuilder.BuildCommand();

        //Act
        await this.sut.Handle(command, CancellationToken.None);

        //Assert
        NoteDto expected = this.noteBuilder.BuildDto();
        this.spyGateway.Notes.Should().ContainEquivalentOf(expected);
    }
}