using Microsoft.AspNetCore.Mvc;
using System;

namespace RecklessSpeech.Web.Configuration.Swagger
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class SwaggerResponseErrorsAttribute : ProducesResponseTypeAttribute
    {
        public SwaggerResponseErrorsAttribute(int statusCode, string errorCode) : base(typeof(string), statusCode) =>
            this.ErrorCode = errorCode;

        public string ErrorCode { get; }
    }
}