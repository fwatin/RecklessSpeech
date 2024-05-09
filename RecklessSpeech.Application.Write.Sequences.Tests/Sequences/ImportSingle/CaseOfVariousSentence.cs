using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.ImportSingle
{
    public class CaseOfVariousSentence
    {
        private readonly InMemorySequenceRepository repository;
        private ImportWordCommandHandler sut;

        public CaseOfVariousSentence()
        {
            this.repository = new();
            this.sut = new(this.repository, new InMemoryMediaRepository());
        }

        [Fact]
        public async Task Case_of_sentence_has_only_one_word()
        {
            //Arrange
            ImportWordCommand command = new(
                "target",
                new[] { "cible" },
                new[] { "target" },
                new[] { "cible" }, new[] { "cible" },
                "mp3.mp3",
                4438, null, null,null);

            //Act
            await this.sut.Handle(command, CancellationToken.None);

            //Assert
            string html = this.repository.All.Single().HtmlContent.Value;
            html.Should().Contain(GetUnderlined("target"));
        }

        [Fact]
        public async Task Case_of_sentence_with_special_characters()
        {
            //Arrange
            ImportWordCommand command = new(
                "target", new[] { "cible" },
                new[] { "this is the target, and the target is here." },
                new[] { "blabla" }, new[] { "blabla" },
                "mp3.mp3",
                4438, null, null,null);

            //Act
            await this.sut.Handle(command, CancellationToken.None);

            //Assert
            string html = this.repository.All.Single().HtmlContent.Value;
            html.Should().Contain(GetNormal("this is the "));
            html.Should().Contain(GetUnderlined("target"), Exactly.Twice());
            html.Should().Contain(GetNormal(" is here."));
        }

        [Fact]
        public async Task Should_separate_translations_with_coma()
        {
            //Arrange
            ImportWordCommand command = new(
                "target", new[] { "cible", "visée", "rabbit" },
                new[] { "this is the target, and the target is here." },
                new[] { "blabla" }, new[] { "blabla" },
                "mp3.mp3",
                4438, null, null,null);

            //Act
            await this.sut.Handle(command, CancellationToken.None);

            //Assert
            ((WordSequence)this.repository.All.Single()).TranslatedWord!.Value.Should().Be("cible, visée, rabbit");
        }

        [Fact]
        public async Task Should_not_take_into_account_the_case()
        {
            //Arrange
            ImportWordCommand command = new(
                "target", new[] { "cible" },
                new[] { "this is the TARGET." },
                new[] { "blabla" }, new[] { "blabla" },
                "mp3.mp3",
                4438, null, null,null);

            //Act
            await this.sut.Handle(command, CancellationToken.None);

            //Assert
            string html = this.repository.All.Single().HtmlContent.Value;
            html.Should().Contain(GetUnderlined("TARGET"));
        }

        private static string GetUnderlined(string word)
        {
            return $"<span class=\"dc-gap\"><span class=\"dc-down dc-lang-en dc-orig\"" +
                   $" style=\"background-color: rgb(157, 0, 0);\">{word}</span></span>";
        }

        private static string GetNormal(string word)
        {
            return $"<span class=\"dc-down dc-lang-en dc-orig\">{word}</span>";
        }
    }
}