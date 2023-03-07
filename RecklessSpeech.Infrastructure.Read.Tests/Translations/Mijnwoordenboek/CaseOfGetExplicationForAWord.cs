using FluentAssertions;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Infrastructure.Sequences.TranslatorGateways.Mijnwoordenboek;
using Xunit;

namespace RecklessSpeech.Infrastructure.Read.Tests.Translations.Mijnwoordenboek
{
    public class CaseOfGetExplicationForAWord
    {
        [Fact]
        public void Should_the_explication_contain_the_most_obvious_translation()
        {
            //Arrange
            string word = "brood";
            IDutchTranslatorGateway gateway = new MijnwoordenboekLocalGateway();

            //Act
            Explanation explanation = gateway.GetExplanation(word);

            //Assert
            explanation.Content.Value.Should().Contain("pain");
            explanation.Content.Value.Should().Contain("voedsel dat je bij de bakker koopt");
        }
    }
}