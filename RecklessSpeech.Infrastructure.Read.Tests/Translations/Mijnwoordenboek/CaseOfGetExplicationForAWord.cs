using FluentAssertions;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Mijnwoordenboek;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Infrastructure.Sequences.TranslatorGateways.Mijnwoordenboek;
using RecklessSpeech.Shared.Tests.Sequences;
using Xunit;

namespace RecklessSpeech.Infrastructure.Read.Tests.Translations.Mijnwoordenboek;

public class CaseOfGetExplicationForAWord
{
    [Fact]
    public void Should_the_explication_contain_the_most_obvious_translation()
    {
        //Arrange
        string word = "brood";
        IMijnwoordenboekGatewayAccess access = new MijnwoordenboekGatewayLocalAccess();
        ITranslatorGateway gateway = new MijnwoordenboekGateway(access);

        //Act
        Explanation explanation = gateway.GetExplanation(word);
        
        //Assert
        explanation.Value.Should().Contain("pain");
        explanation.Value.Should().Contain("voedsel dat je bij de bakker koopt");
    }
}