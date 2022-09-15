using FluentAssertions;
using RecklessSpeech.Front.WPF.App.ViewModels;
using Xunit;

namespace RecklessSpeech.Front.Tests.SequencePage.ViewModel;

public class CaseOfSuccessful
{
    [Fact]
    public void Should_sequences_display_only_get_all_content_and_clear_former_sequences()
    {
        //Arrange
        HttpBackEndGateway gateway = new(new SpyBackEndGatewayAccess());
        SequencePageViewModel viewModel = new(gateway);
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "gimmicks.csv");
        viewModel.AddSequencesCommand.Execute(filePath);
        
        //Act
        viewModel.AddSequencesCommand.Execute(filePath);
        
        //Assert
        viewModel.Sequences.Should().HaveCount(1);
    }
}