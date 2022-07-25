using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecklessSpeech.Application.Write.Sequences.Commands;

namespace RecklessSpeech.Web;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/sequences")]
[ApiController]
public class ImportSequenceController : ControllerBase
{
    private readonly WebDispatcher dispatcher;

    public ImportSequenceController(WebDispatcher dispatcher)
    {
        this.dispatcher = dispatcher;
    }
    
    [HttpPost]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<string>> ImportSequences(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        var data = await reader.ReadToEndAsync();
        var command = new ImportSequencesCommand(data);

        await this.dispatcher.Dispatch(command);

        return this.Ok();
    }
}