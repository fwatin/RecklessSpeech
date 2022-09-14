using FluentAssertions;
using RecklessSpeech.Application.Write.Sequences.Commands;
using RecklessSpeech.Domain.Sequences.Notes;
using RecklessSpeech.Infrastructure.Databases;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Shared.Tests.Explanations;
using RecklessSpeech.Shared.Tests.Notes;
using RecklessSpeech.Shared.Tests.Sequences;
using Xunit;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Notes.Send;

public class CaseOfNewNotes
{
    private readonly SendNotesCommandHandler sut;
    private readonly SpyNoteGateway spyGateway;
    private readonly InMemorySequencesDbContext dbContext;
    private InMemorySequenceRepository sequenceRepository;
    public CaseOfNewNotes()
    {
        this.spyGateway = new();
        this.dbContext = new InMemorySequencesDbContext();
        this.sequenceRepository = new(this.dbContext);
        this.sut = new(this.spyGateway, sequenceRepository);
    }
    
    [Fact]
    public async Task Should_send_a_new_note_to_Anki()
    {
        //Arrange
        Guid sequenceId = Guid.Parse("79FAD304-21BC-4B58-BECF-0884016DCC11");
        const string someHtml = "\"<style> some html here for this test\"";
        SequenceBuilder sequenceBuilder = SequenceBuilder.Create(sequenceId) with
        {
            HtmlContent = new(someHtml),
            TranslatedSentence = new("translation")
        };
        SequenceEntity sequenceEntity = sequenceBuilder.BuildEntity();
        this.dbContext.Sequences.Add(sequenceEntity);

        NoteBuilder noteBuilder = NoteBuilder.Create(sequenceId) with
        {
            Question = new(someHtml),
            After = new("translated sentence from Netflix: \"translation\"")
        };
        SendNotesCommand command = noteBuilder.BuildCommand();

        //Act
        await this.sut.Handle(command, CancellationToken.None);

        //Assert
        NoteDto expected = noteBuilder.BuildDto();
        this.spyGateway.Notes.Should().ContainEquivalentOf(expected);
    }
    
    [Fact]
    public async Task Should_after_field_contain_translated_sentence()
    {
        //Arrange
        SequenceBuilder sequenceBuilder = SequenceBuilder.Create(Guid.Parse("B03B23B5-EB9F-4EB8-A762-308A39ADA735"));
        SequenceEntity sequenceEntity = sequenceBuilder.BuildEntity();
        this.dbContext.Sequences.Add(sequenceEntity);
        NoteBuilder noteBuilder = NoteBuilder.Create(sequenceBuilder.SequenceId.Value);
        SendNotesCommand command = noteBuilder.BuildCommand();

        //Act
        await this.sut.Handle(command, CancellationToken.None);

        //Assert
        this.spyGateway.Notes.Single().After.Value.Should().Contain(sequenceBuilder.TranslatedSentence.Value);
    }
}