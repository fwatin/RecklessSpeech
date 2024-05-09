#nullable enable
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Application.Read.Queries.Sequences.GetOne;
using RecklessSpeech.Application.Write.Sequences.Commands.Notes.SendToAnki;
using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.AddDetails;
using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Enrich;
using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Phrases;
using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Sequences;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RecklessSpeech.Web.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/sequences")]
    [ApiController]
    public class SequenceController : ControllerBase
    {
        private readonly IMediator dispatcher;

        public SequenceController(IMediator dispatcher) => this.dispatcher = dispatcher;

        [HttpPost]
        [Route("import-json/")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IReadOnlyCollection<SequenceSummaryQueryModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<SequenceSummaryQueryModel>>> ImportJson(IFormFile file)
        {
            Class1[]? payload;
            try
            {
                using StreamReader reader = new(file.OpenReadStream());
                string data = await reader.ReadToEndAsync();
                payload = JsonConvert.DeserializeObject<Class1[]>(data);
            }
            catch (Exception e)
            {
                return this.BadRequest($"error while reading and deserializing json: + {e.Message}");
            }

            foreach (Class1 item in payload)
            {
                try
                {
                    string? leftImageBase64 = item.context?.phrase?.thumb_prev?.dataURL.Split(',').Last();
                    string? rightImageBase64 = item.context?.phrase?.thumb_next?.dataURL.Split(',').Last();
                    string? mp3Base64 = item.audio?.dataURL?.Split(',').Last();

                    if (item.itemType == "WORD")
                    {
                        int? wordIndex = item.context?.wordIndex;
                        Subtitletokens? correspondingToken = wordIndex is not null
                            ? item.context?.phrase?.subtitleTokens["1"][wordIndex.Value]
                            : null;

                        ImportWordCommand importWord = new(
                            correspondingToken?.form.text,
                            item.wordTranslationsArr,
                            item.context!.phrase!.subtitles.Values.ToArray(),
                            item.context!.phrase!.hTranslations?.Values.ToArray(),
                            item.context!.phrase!.mTranslations?.Values.ToArray(),
                            item.context.phrase.reference.title,
                            item.timeModified_ms,
                            leftImageBase64,
                            rightImageBase64,
                            mp3Base64,item.langCode_G);

                        await this.dispatcher.Send(importWord);
                    }
                    else if (item.itemType == "PHRASE")
                    {
                        ImportPhraseCommand importPhrase = new(
                            item.context!.phrase!.subtitles["1"],
                            item.context!.phrase!.subtitles.Values.ToArray(),
                            item.context!.phrase!.hTranslations?.Values.ToArray(),
                            item.context!.phrase!.mTranslations?.Values.ToArray(),
                            item.context.phrase.reference.title,
                            item.timeModified_ms,
                            leftImageBase64,
                            rightImageBase64,
                            mp3Base64, item.langCode_G);
                        await this.dispatcher.Send(importPhrase);
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine($"error while importing {item.word.text} : {e.Message}");
                }
            }

            IReadOnlyCollection<SequenceSummaryQueryModel> result =
                await this.dispatcher.Send(new GetAllSequencesQuery());
            return this.Ok(result);
        }

        [HttpPost]
        [Route("enrich-and-send-to-anki")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<SequenceSummaryQueryModel>> EnrichAndSendToAnki([FromQuery] Guid id)
        {
            try
            {
                await this.dispatcher.Send(new EnrichSequenceCommand(id));

                await this.dispatcher.Send(new SendNoteToAnkiCommand(id));

                SequenceSummaryQueryModel result = await this.dispatcher.Send(new GetOneSequenceQuery(new(id)));

                return this.Ok(result);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }

        [HttpGet]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IReadOnlyCollection<SequenceSummaryQueryModel>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<SequenceSummaryQueryModel>>> Get()
        {
            IReadOnlyCollection<SequenceSummaryQueryModel> result =
                await this.dispatcher.Send(new GetAllSequencesQuery());
            return this.Ok(result);
        }
    }
}