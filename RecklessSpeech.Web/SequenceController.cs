using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Application.Write.Sequences.Commands;
using RecklessSpeech.Web.Sequences;
using RecklessSpeech.Web.ViewModels.Sequences;

namespace RecklessSpeech.Web;

[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/sequences")]
[ApiController]
public class SequenceController : ControllerBase
{
    private readonly WebDispatcher dispatcher;

    public SequenceController(WebDispatcher dispatcher)
    {
        this.dispatcher = dispatcher;
    }

    [HttpPost]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<string>> ImportSequences(IFormFile file)
    {
        using var reader = new StreamReader(file.OpenReadStream());
        var data = await reader.ReadToEndAsync();
        var command = new ImportSequencesCommand(data);

        await this.dispatcher.Dispatch(command);

        return this.Ok();
    }

    [HttpGet]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(IReadOnlyCollection<SequenceSummaryPresentation>), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyCollection<SequenceSummaryPresentation>>> Get()
    {
        var result = await this.dispatcher.Dispatch(new GetAllSequencesQuery());
        return this.Ok(result.ToPresentation());
    }

    [HttpPost]
    [Route("Anki/")]
    [MapToApiVersion("1.0")]
    [ProducesResponseType(typeof(string), (int) HttpStatusCode.OK)]
    public async Task<ActionResult<IReadOnlyCollection<SequenceSummaryPresentation>>> SendToAnki(
        IReadOnlyCollection<Guid> ids)
    {
        await this.dispatcher.Dispatch(new SendNotesCommand(ids));
        return this.Ok();
    }
}