using RecklessSpeech.Application.Write.Sequences.Commands.Notes.Send;
using RecklessSpeech.Shared.Tests.Notes;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Notes.Send
{
    public class CaseOfNewNotes
    {
        private readonly InMemoryTestSequenceRepository sequenceRepository;
        private readonly SpyNoteGateway spyGateway;
        private readonly SendNotesCommandHandler sut;

        public CaseOfNewNotes()
        {
            this.spyGateway = new();
            this.sequenceRepository = new();
            this.sut = new(this.spyGateway, this.sequenceRepository);
        }

        [Fact]
        public async Task Should_after_field_contain_translated_sentence()
        {
            //Arrange
            SequenceBuilder sequenceBuilder =
                SequenceBuilder.Create(Guid.Parse("B03B23B5-EB9F-4EB8-A762-308A39ADA735")) with
                {
                    Explanation = ExplanationBuilder.Create()
                };
            this.sequenceRepository.Feed(sequenceBuilder.BuildDomain());
            NoteBuilder noteBuilder = NoteBuilder.Create(sequenceBuilder.SequenceId.Value);
            SendNotesCommand command = noteBuilder.BuildCommand();

            //Act
            await this.sut.Handle(command, CancellationToken.None);

            //Assert
            this.spyGateway.Note!.After.Value.Should().Contain(sequenceBuilder.TranslatedSentence.Value);
        }

        [Fact]
        public async Task Should_note_contains_url_of_dictionary_in_source()
        {
            //Arrange
            ExplanationBuilder explanationBuilder =
                ExplanationBuilder.Create(Guid.Parse("684F35A0-B472-4D5A-8C42-74C4646490CB"));
            SequenceBuilder sequenceBuilder =
                SequenceBuilder.Create(Guid.Parse("B03B23B5-EB9F-4EB8-A762-308A39ADA735")) with
                {
                    Explanation = ExplanationBuilder.Create(explanationBuilder.ExplanationId.Value)
                };
            this.sequenceRepository.Feed(sequenceBuilder.BuildDomain());

            NoteBuilder noteBuilder = NoteBuilder.Create(sequenceBuilder.SequenceId.Value);
            SendNotesCommand command = noteBuilder.BuildCommand();

            //Act
            await this.sut.Handle(command, CancellationToken.None);

            //Assert
            const string expectedUrl = "https://www.mijnwoordenboek.nl/vertaal/NL/FR/gimmicks";
            this.spyGateway.Note!.Source.Value.Should().Be($"<a href=\"{expectedUrl}\">{expectedUrl}</a>");
        }
    }
}