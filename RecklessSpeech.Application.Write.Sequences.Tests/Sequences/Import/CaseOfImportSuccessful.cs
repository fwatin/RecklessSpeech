using System.Text;
using FluentAssertions;
using RecklessSpeech.Domain.Shared;
using Xunit;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.Import;

public class CaseOfImportSuccessful
{
    private readonly ImportSequencesCommandHandler sut;

    public CaseOfImportSuccessful()
    {
        this.sut = new ImportSequencesCommandHandler();
    }

    [Fact]
    public async Task Should_add_a_new_sequence()
    {
        //Arrange
        string content =
            "\"a lot of things in html\"	[sound:1658501397855.mp3]	\"word-naked lang-nl netflix Green pron \"";
        ImportSequencesCommand command = new(content);
        SequencesImportRequestedEvent expected = new("\"a lot of things in html\"",
            AudioFileNameWithExtension.Hydrate("1658501397855.mp3"),
            "\"word-naked lang-nl netflix Green pron \"");

        //Act
        IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command);

        //Assert
        events.Should().ContainEquivalentOf(expected);
    }
}

public record ImportSequencesCommand(string FileContent);