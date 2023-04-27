using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Application.Read.Queries.Sequences.GetOne;
using RecklessSpeech.Application.Write.Sequences.Commands.Notes.SendToAnki;
using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.AddDetails;
using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Enrich;
using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import;
using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Media;
using RecklessSpeech.Web.Configuration;
using RecklessSpeech.Web.Configuration.Swagger;
using RecklessSpeech.Web.ViewModels.Sequences;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace RecklessSpeech.Web.Sequences
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/sequences")]
    [ApiController]
    public class SequenceController : ControllerBase
    {
        private readonly IMediator dispatcher;

        public SequenceController(IMediator dispatcher) => this.dispatcher = dispatcher;

        [HttpPost]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IReadOnlyCollection<SequenceSummaryQueryModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<SequenceSummaryQueryModel>>> ImportSequences(IFormFile file)
        {
            try
            {
                using StreamReader reader = new(file.OpenReadStream());
                string data = await reader.ReadToEndAsync();
                ImportSequencesCommand command = new(data);

                await this.dispatcher.Send(command);

                IReadOnlyCollection<SequenceSummaryQueryModel> all =
                    await this.dispatcher.Send(new GetAllSequencesQuery());

                return this.Ok(all);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("import-zip/")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IReadOnlyCollection<SequenceSummaryQueryModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<SequenceSummaryQueryModel>>> ImportSequencesWithZip(IFormFile file)
        {
            try
            {
                using MemoryStream memoryStream = new();
                await file.CopyToAsync(memoryStream);

                memoryStream.Seek(0, SeekOrigin.Begin);

                using ZipArchive archive = new(memoryStream, ZipArchiveMode.Read);
                List<SequenceSummaryQueryModel> all = new();
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    await using Stream entryStream = entry.Open();
                    byte[] buffer = new byte[entryStream.Length];
                    int bytesRead = await entryStream.ReadAsync(buffer);

                    if (bytesRead != entryStream.Length)
                    {
                        return this.BadRequest($"Erreur lors de la lecture du fichier {entry.FullName}");
                    }

                    if (entry.FullName == "items.csv")
                    {


                        string data = Encoding.UTF8.GetString(buffer);

                        ImportSequencesCommand command = new(data);

                        await this.dispatcher.Send(command);

                        all.AddRange(await this.dispatcher.Send(new GetAllSequencesQuery()));
                    }
                    else
                    {
                        SaveMediaCommand saveMedia = new(entry.FullName, buffer);
                        await this.dispatcher.Send(saveMedia);
                    }
                }

                return this.Ok(all);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }


        [HttpPost]
        [Route("import-details/")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IReadOnlyCollection<SequenceSummaryQueryModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<SequenceSummaryQueryModel>>> ImportDetails(IFormFile file)
        {
            try
            {
                using StreamReader reader = new(file.OpenReadStream());
                string data = await reader.ReadToEndAsync();
                Class1[]? sequenceDetailsDto = JsonConvert.DeserializeObject<Class1[]>(data);
                AddDetailsToSequencesCommand command = new(sequenceDetailsDto!);
                await this.dispatcher.Send(command);

                IReadOnlyCollection<SequenceSummaryQueryModel> r = await this.dispatcher.Send(new GetAllSequencesQuery());
                return this.Ok(r);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IReadOnlyCollection<SequenceSummaryPresentation>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<SequenceSummaryPresentation>>> Get()
        {
            IReadOnlyCollection<SequenceSummaryQueryModel> result =
                await this.dispatcher.Send(new GetAllSequencesQuery());
            return this.Ok(result.ToPresentation());
        }

        [HttpGet("{sequenceId:guid}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(SequenceSummaryPresentation), (int)HttpStatusCode.OK)]
        [SwaggerResponseErrors((int)HttpStatusCode.NotFound, ApiErrors.ReadSequenceNotFound)]
        public async Task<ActionResult<SequenceSummaryPresentation>> GetOne(Guid sequenceId)
        {
            SequenceSummaryQueryModel result = await this.dispatcher.Send(new GetOneSequenceQuery(new(sequenceId)));
            return this.Ok(result.ToPresentation());
        }

        [HttpPost]
        [Route("send-to-anki/")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<SequenceSummaryPresentation>>> SendToAnki(
            [FromQuery] Guid id)
        {
            try
            {
                await this.dispatcher.Send(new SendNoteToAnkiCommand(id));
                return this.Ok();
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("Dictionary/dutch")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<SequenceSummaryPresentation>>> EnrichDutch(
            [FromQuery] Guid id)
        {
            await this.dispatcher.Send(new EnrichDutchSequenceCommand(id));
            return this.Ok();
        }

        [HttpPost]
        [Route("Dictionary/english")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<SequenceSummaryPresentation>>> EnrichEnglish(
            [FromQuery] Guid id)
        {
            await this.dispatcher.Send(new EnrichEnglishSequenceCommand(id));
            return this.Ok();
        }
    }
}