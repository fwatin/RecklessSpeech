using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecklessSpeech.Application.Read.Queries.Notes.GetByTag;
using RecklessSpeech.Application.Write.Sequences.Commands.Notes.ReverseNote;
using RecklessSpeech.Domain.Sequences.Notes;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RecklessSpeech.Web.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/notes")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly IMediator dispatcher;
        public NoteController(IMediator dispatcher) => this.dispatcher = dispatcher;


        [HttpPost]
        [Route("reverse-blue-flagged")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(ReverseResult), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<string>> Reverse()
        {
            IReadOnlyCollection<Note> notes = ArraySegment<Note>.Empty;
            List<string> succeeded = new();
            List<string> failed = new();
            try
            {
                notes = await this.dispatcher.Send(new GetNotesWithBlueFlagQuery());
            }

            catch (Exception)
            {
                Console.WriteLine($"error while getting notes with blue flag");
            }

            foreach (Note note in notes)
            {
                try
                {
                    ReverseNoteResult result = await this.dispatcher.Send(new ReverseNoteCommand(note));
                    if (result.HasBeenReversed) succeeded.Add(result.Word);
                }
                catch (Exception)
                {
                    failed.Add(note.GetDto().Answer.Value);
                }
            }

            return this.Ok(new ReverseResult(succeeded, failed));
        }
    }

    internal record ReverseResult(List<string> Succeeded, List<string> Failed);
}