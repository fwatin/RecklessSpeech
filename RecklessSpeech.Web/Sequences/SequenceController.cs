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
        [Route("import-zip/")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IReadOnlyCollection<SequenceSummaryQueryModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<SequenceSummaryQueryModel>>> ImportSequencesWithZip(
            IFormFile file)
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

                IReadOnlyCollection<SequenceSummaryQueryModel> r =
                    await this.dispatcher.Send(new GetAllSequencesQuery());
                return this.Ok(r);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpPost]
        [Route("enrich-and-send-to-anki/{language}")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SequenceSummaryQueryModel>> EnrichAndSendToAnki([FromQuery] Guid id,
            string language)
        {
            try
            {
                switch (language)
                {
                    case "english":
                        await this.dispatcher.Send(new EnrichEnglishSequenceCommand(id));
                        break;
                    case "dutch":
                        await this.dispatcher.Send(new EnrichDutchSequenceCommand(id));
                        break;
                }

                await this.dispatcher.Send(new SendNoteToAnkiCommand(id));

                SequenceSummaryQueryModel result = await this.dispatcher.Send(new GetOneSequenceQuery(new(id)));
                return this.Ok(result);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}