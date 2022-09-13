using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Hosting;
using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Application.Read.Queries.Sequences.GetOne;
using RecklessSpeech.Infrastructure.Read;
using RecklessSpeech.Web.Configuration;

namespace RecklessSpeech.Web.Controllers
{
    [ApiVersion("1.0")]
    [ApiController]
    public class ErrorController : ControllerBase
    {
        private readonly ProblemDetailsFactory problemDetailsFactory;
        private readonly IHostEnvironment hostEnvironment;

        public ErrorController(
            ProblemDetailsFactory problemDetailsFactory,
            IHostEnvironment hostEnvironment)
        {
            this.problemDetailsFactory = problemDetailsFactory;
            this.hostEnvironment = hostEnvironment;
        }

        [MapToApiVersion("1.0")]
        [Route("/error"), ApiExplorerSettings(IgnoreApi = true), AllowAnonymous]
        public IActionResult Error()
        {
            IExceptionHandlerFeature? context = this.HttpContext.Features.Get<IExceptionHandlerFeature>();

            if (context == null)
                return this.StatusCode(StatusCodes.Status500InternalServerError);

            return context.Error switch
            {
                SequenceNotFoundReadException exception => this.Handle(exception),
                { } exception => this.Handle(exception)
            };
            
            
        }

        private IActionResult Handle(SequenceNotFoundReadException exception) =>
            this.HandleError(StatusCodes.Status404NotFound, ApiErrors.ReadSequenceNotFound, exception);
        
        private IActionResult Handle(Exception exception) =>
            this.HandleError(StatusCodes.Status500InternalServerError, ApiErrors.GenericInternalServerError, exception);

        private IActionResult HandleError(int statusCode, string type, Exception exception)
        {
            ProblemDetails problemDetails = this.problemDetailsFactory.CreateProblemDetails(this.HttpContext, statusCode, exception.Message, type);
            if (this.hostEnvironment.IsDevelopment())
            {
                problemDetails.Extensions.Add("exception", RenderException(exception));
            }

            return this.StatusCode(problemDetails.Status!.Value, problemDetails);
        }

        private static object RenderException(Exception e)
        {
            return new
            {
                e.Message,
                e.Data,
                InnerException = e.InnerException != null ? RenderException(e.InnerException) : null,
                e.StackTrace,
                HResult = e.HResult.ToString("X"),
                e.Source,
                TargetSite = e.TargetSite?.ToString()
            };
        }
    }
}