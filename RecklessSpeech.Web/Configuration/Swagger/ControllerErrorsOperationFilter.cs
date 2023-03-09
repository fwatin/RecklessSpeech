using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace RecklessSpeech.Web.Configuration.Swagger
{
    internal class ControllerErrorsOperationFilter : IOperationFilter
    {
        public void Apply(OpenApiOperation operation, OperationFilterContext context)
        {
            foreach (IGrouping<int, SwaggerResponseErrorsAttribute> actionAttributeByStatusCode in
                     context
                         .MethodInfo
                         .GetCustomAttributes<SwaggerResponseErrorsAttribute>()
                         .GroupBy(a => a.StatusCode))
            {
                SetResponseErrorForStatusCode(
                    operation,
                    actionAttributeByStatusCode.Key,
                    actionAttributeByStatusCode
                        .Select(a => a.ErrorCode)
                        .OrderBy(e => e)
                        .ToList());
            }
        }

        private static void SetResponseErrorForStatusCode(
            OpenApiOperation operation,
            int statusCode,
            IReadOnlyCollection<string> errors)
        {
            if (errors.Count == 0)
            {
                return;
            }

            string key = statusCode == 0 ? "default" : statusCode.ToString();
            KeyValuePair<string, OpenApiResponse> response = operation.Responses.FirstOrDefault(r => r.Key == key);

            if (response.Equals(default(KeyValuePair<string, OpenApiResponse>)))
            {
                return;
            }

            SetResponseErrorForStatusCode(response, errors);
        }

        private static void SetResponseErrorForStatusCode(
            KeyValuePair<string, OpenApiResponse> response,
            IEnumerable<string> errors)
        {
            StringBuilder builder = new();
            builder.Append("<p>");
            builder.Append(response.Value.Description);
            builder.Append("</p><p>");
            builder.Append("Error codes:<ul>");
            foreach (string error in errors)
            {
                if (string.IsNullOrWhiteSpace(error))
                {
                    continue;
                }

                builder.Append("<li>");
                builder.Append(error);
                builder.Append("</li>");
            }

            builder.Append("</ul></p>");
            response.Value.Description = builder.ToString();
        }
    }
}