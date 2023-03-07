using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace RecklessSpeech.AcceptanceTests.Configuration
{
    public class HttpTestServerException : Exception
    {
        public HttpTestServerException(HttpStatusCode statusCode, ProblemDetails details)
            : base($"Error {(int)statusCode} : {details}")
        {
            this.StatusCode = statusCode;
            this.Details = details;
        }

        public HttpStatusCode StatusCode { get; }
        public ProblemDetails Details { get; }
    }
}