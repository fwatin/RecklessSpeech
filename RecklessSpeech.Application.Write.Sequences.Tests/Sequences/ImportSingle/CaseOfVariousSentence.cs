﻿using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.ImportSingle
{
    public class CaseOfVariousSentence
    {
        private readonly InMemorySequenceRepository repository;
        private ImportSequenceCommandHandler sut;

        public CaseOfVariousSentence()
        {
            this.repository = new InMemorySequenceRepository();
            this.sut = new(this.repository);
        }

        [Fact]
        public async Task Case_of_sentence_has_only_one_word()
        {
            //Arrange
            ImportSequenceCommand command = new(
                "target",
                "target",
                "mp3.mp3",
                4438);

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
            ImportSequenceCommand command = new(
                "target",
                "this is the target.",
                "mp3.mp3",
                4438);

            //Act
            await this.sut.Handle(command, CancellationToken.None);

            //Assert
            string html = this.repository.All.Single().HtmlContent.Value;
            html.Should().Contain(GetNormal("this is the "));
            html.Should().Contain(GetUnderlined("target"));
            html.Should().Contain(GetNormal("."));
        }

        private static string GetUnderlined(string word)
        {
            return  $"<span class=\"dc-gap\"><span class=\"dc-down dc-lang-en dc-orig\"" +
                                    $" style=\"background-color: rgb(157, 0, 0);\">{word}</span></span>";
        }
        
        private static string GetNormal(string word)
        {
            return $"<span class=\"dc-down dc-lang-en dc-orig\">{word}</span>";
        }
    }
}