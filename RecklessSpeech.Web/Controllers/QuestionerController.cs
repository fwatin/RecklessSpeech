using MediatR;
using Microsoft.AspNetCore.Mvc;
using RecklessSpeech.Application.Write.Questioner.Commands.ExamineCompletion;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace RecklessSpeech.Web.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v1/questioner")]
    [ApiController]
    public class QuestionerController : ControllerBase
    {
        private readonly IMediator dispatcher;

        public QuestionerController(IMediator dispatcher) => this.dispatcher = dispatcher;


        [HttpPost]
        [Route("analyse-completion/")]
        [MapToApiVersion("1.0")]
        [ProducesResponseType(typeof(IReadOnlyCollection<string>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<IReadOnlyCollection<string>>> AnalyseCompletion(string subject,
            string completion)
        {
            try
            {
                var result = await this.dispatcher.Send(new ExamineCompletionCommand(new(completion), subject));

                return this.Ok(result);
            }
            catch (Exception e)
            {
                return this.BadRequest(e.Message);
            }
        }
    }
}