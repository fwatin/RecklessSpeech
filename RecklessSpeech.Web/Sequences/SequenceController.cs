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
using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Media;
using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Sequences;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace RecklessSpeech.Web.Sequences
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
                    string? prevBase64 = item.context?.phrase?.thumb_prev?.dataURL.Split(',').Last();
                    if (prevBase64 is not null)
                    {
                        byte[] prev = Convert.FromBase64String(prevBase64);
                        SaveMediaCommand savePrev = new($"{item.timeModified_ms}_prev.jpg", prev);
                        await this.dispatcher.Send(savePrev);
                    }

                    string? nextBase64 = item.context?.phrase?.thumb_next?.dataURL.Split(',').Last();
                    if (nextBase64 is not null)
                    {
                        byte[] next = Convert.FromBase64String(nextBase64);
                        SaveMediaCommand saveNext = new($"{item.timeModified_ms}_next.jpg", next);
                        await this.dispatcher.Send(saveNext);
                    }

                    //mp3
                    string mp3InBase64 = item.audio.dataURL.Split(',').Last();
                    byte[] mp3 = Convert.FromBase64String(mp3InBase64);
                    SaveMediaCommand saveMp3 = new($"{item.timeModified_ms}.mp3", mp3);
                    await this.dispatcher.Send(saveMp3);

                    //sequence
                    TranslationDto translation = new TranslationDto(
                        item.context!.phrase!.hTranslations?.Values.ToArray(),
                        item.context!.phrase!.mTranslations?.Values.ToArray());

                    int wordIndex = item.context.wordIndex;
                    var correspondingToken = item.context.phrase.subtitleTokens["1"][wordIndex];
                    
                    ImportSequenceCommand import = new(correspondingToken.form.text,
                        item.wordTranslationsArr,
                        item.context!.phrase!.subtitles.Values.ToArray(),
                        translation,
                        item.context.phrase.reference.title,
                        item.timeModified_ms);

                    await this.dispatcher.Send(import);
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
                    case "italian":
                        await this.dispatcher.Send(new EnrichItalianSequenceCommand(id));
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