using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Application.Read.Queries.Sequences.GetOne;
using RecklessSpeech.Application.Write.Sequences.Commands.Notes.SendToAnki;
using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.AddDetails;
using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Enrich;
using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import;
using RecklessSpeech.Web.Configuration;
using RecklessSpeech.Web.Configuration.Swagger;
using RecklessSpeech.Web.ViewModels.Sequences;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RecklessSpeech.Web.Sequences
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/sequences")]
    [ApiController]
    public class SequenceController : ControllerBase
    {
        private readonly WebDispatcher dispatcher;

        public SequenceController(WebDispatcher dispatcher) => this.dispatcher = dispatcher;

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> ImportSequences(IFormFile file)
        {
            using StreamReader reader = new(file.OpenReadStream());
            string data = await reader.ReadToEndAsync();
            ImportSequencesCommand command = new(data);

            await this.dispatcher.Dispatch(command);

            return this.Ok();
        }

        [HttpPost]
        [Route("import-details/")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> ImportDetails(IFormFile file)
        {
            using StreamReader reader = new(file.OpenReadStream());
            string data = await reader.ReadToEndAsync();
            Class1[]? sequenceDetailsDto = JsonConvert.DeserializeObject<Class1[]>(data);
            AddDetailsToSequencesCommand command = new(sequenceDetailsDto!);
            await this.dispatcher.Dispatch(command);
            return this.Ok();
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IReadOnlyCollection<SequenceSummaryPresentation>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<SequenceSummaryPresentation>>> Get()
        {
            IReadOnlyCollection<SequenceSummaryQueryModel> result =
                await this.dispatcher.Dispatch(new GetAllSequencesQuery());
            return this.Ok(result.ToPresentation());
        }

        [HttpGet("{sequenceId:guid}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(SequenceSummaryPresentation), (int)HttpStatusCode.OK)]
        [SwaggerResponseErrors((int)HttpStatusCode.NotFound, ApiErrors.ReadSequenceNotFound)]
        public async Task<ActionResult<SequenceSummaryPresentation>> GetOne(Guid sequenceId)
        {
            SequenceSummaryQueryModel result = await this.dispatcher.Dispatch(new GetOneSequenceQuery(new(sequenceId)));
            return this.Ok(result.ToPresentation());
        }

        [HttpPost]
        [Route("send-to-anki/")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<SequenceSummaryPresentation>>> SendToAnki([FromQuery] Guid id)
        {
            try
            {
                await this.dispatcher.Dispatch(new SendNoteToAnkiCommand(id));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return this.BadRequest(e.Message);
            }
            return this.Ok();
        }

        [HttpPost]
        [Route("Dictionary/dutch")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<SequenceSummaryPresentation>>> EnrichDutch(
            IReadOnlyCollection<Guid> ids)
        {
            await this.dispatcher.Dispatch(new EnrichDutchSequenceCommand(ids.First()));
            return this.Ok();
        }

        [HttpPost]
        [Route("Dictionary/english")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<SequenceSummaryPresentation>>> EnrichEnglish(
            IReadOnlyCollection<Guid> ids)
        {
            await this.dispatcher.Dispatch(new EnrichEnglishSequenceCommand(ids.First()));
            return this.Ok();
        }
    }
}