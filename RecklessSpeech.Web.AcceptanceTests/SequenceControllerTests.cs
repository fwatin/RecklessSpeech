using MediatR;
using Microsoft.AspNetCore.Http;
using NSubstitute;
using RecklessSpeech.Web.Controllers;

namespace RecklessSpeech.Web.AcceptanceTests;

public class SequenceControllerTests
{
    [Fact]
    public async Task ImportSinglePhrase()
    {
        //Arrange
        IMediator dispatcher = Substitute.For<IMediator>();
        SequenceController controller = new(dispatcher);
        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "Data", "single-phrase.json");
        IFormFile formFile;
        await using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            formFile = new FormFile(stream, 0, stream.Length, "file", Path.GetFileName(filePath));
            var result = await controller.ImportJson(formFile);

        }

        //Act
    }
}