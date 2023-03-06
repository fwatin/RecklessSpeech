using FluentAssertions;
using RecklessSpeech.Application.Write.Sequences.Commands;
using RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Repositories;
using RecklessSpeech.Domain.Sequences.Notes;
using RecklessSpeech.Infrastructure.Databases;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Shared.Tests.Explanations;
using RecklessSpeech.Shared.Tests.Notes;
using RecklessSpeech.Shared.Tests.Sequences;
using Xunit;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Notes.Send;

public class CaseOfANewNote
{
    private const string SomeHtml = "\"<style> some html here for this test\"";
    private readonly SendNotesCommandHandler sut;
    private readonly SpyNoteGateway spyGateway;
    private readonly InMemoryTestSequenceRepository sequenceRepository;
    private readonly Guid sequenceId;
    private SendNotesCommand command;

    public CaseOfANewNote()
    {
        this.spyGateway = new();
        this.sequenceRepository = new();
        this.sut = new(this.spyGateway, this.sequenceRepository);

        sequenceId = Guid.Parse("79FAD304-21BC-4B58-BECF-0884016DCC11");
        command = new(new List<Guid>() { sequenceId });
    }

    [Fact]
    public async Task Should_contain_only_one()
    {
        //Arrange
        SequenceBuilder sequenceBuilder = SequenceBuilder.Create(sequenceId);
        this.sequenceRepository.Feed(sequenceBuilder);

        //Act
        await this.sut.Handle(command, CancellationToken.None);

        //Assert
        this.spyGateway.Notes.Should().HaveCount(1);
    }
    
    [Fact]
    public async Task Should_contains_html_in_question()
    {
        //Arrange
        SequenceBuilder sequenceBuilder = SequenceBuilder.Create(sequenceId) with
        {
            HtmlContent = new("\"<style> some html here for this test\""),
        };
        this.sequenceRepository.Feed(sequenceBuilder);

        //Act
        await this.sut.Handle(command, CancellationToken.None);

        //Assert
        this.spyGateway.Notes.First().Question.Value.Should().Be(sequenceBuilder.HtmlContent.Value);
    }
    
    [Fact]
    public async Task Should_contains_answer_if_translated_word()
    {
        //Arrange
        SequenceBuilder sequenceBuilder = SequenceBuilder.Create(sequenceId) with
        {
            TranslatedWord = new("pain")
        };
        this.sequenceRepository.Feed(sequenceBuilder);

        //Act
        await this.sut.Handle(command, CancellationToken.None);

        //Assert
        this.spyGateway.Notes.First().Answer.Value.Should().Be("pain");
    }
    
    [Fact]
    public async Task Should_contains_explanation_in_after()
    {
        //Arrange
        SequenceBuilder sequenceBuilder = SequenceBuilder.Create(sequenceId) with
        {
            TranslatedSentence = new("hey this is the translated sentence from Netflix"),
            Explanation = ExplanationBuilder.Create() with{Content = new("a lot of explanations")}
        };
        this.sequenceRepository.Feed(sequenceBuilder);

        //Act
        await this.sut.Handle(command, CancellationToken.None);

        //Assert
        this.spyGateway.Notes.First().After.Value.Trim().Should().Be($"translated sentence from Netflix: \"hey this is the translated sentence from Netflix\"a lot of explanations");
    }
    
    [Fact]
    public async Task Should_contains_source_from_explanation()
    {
        //Arrange
        SequenceBuilder sequenceBuilder = SequenceBuilder.Create(sequenceId) with
        {
            Explanation =ExplanationBuilder.Create()with{SourceUrl = new("www.farfelu.com/translation")}
        };
        this.sequenceRepository.Feed(sequenceBuilder);

        //Act
        await this.sut.Handle(command, CancellationToken.None);

        //Assert
        this.spyGateway.Notes.First().Source.Value.Should().Be("<a href=\"www.farfelu.com/translation\">www.farfelu.com/translation</a>");
    }
    
    [Fact]
    public async Task Should_contains_audio()
    {
        //Arrange
        SequenceBuilder sequenceBuilder = SequenceBuilder.Create(sequenceId) with
        {
            AudioFileNameWithExtension = new("368468486.mp3")
        };
        this.sequenceRepository.Feed(sequenceBuilder);

        //Act
        await this.sut.Handle(command, CancellationToken.None);

        //Assert
        this.spyGateway.Notes.First().Audio.Value.Should().Be("[sound:368468486.mp3]");
    }
}