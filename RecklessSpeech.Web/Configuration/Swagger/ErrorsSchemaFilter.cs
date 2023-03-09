using Microsoft.OpenApi.Any;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Linq;

namespace RecklessSpeech.Web.Configuration.Swagger
{
    public class ErrorsSchemaFilter : ISchemaFilter
    {
        public void Apply(OpenApiSchema schema, SchemaFilterContext context)
        {
            if (context.Type != typeof(ApiErrors))
            {
                return;
            }

            schema.Properties["type"] = new()
            {
                Type = "string",
                Enum = ApiErrors.Errors()
                    .OrderBy(et => et)
                    .Select(et => new OpenApiString(et))
                    .Cast<IOpenApiAny>()
                    .ToList()
            };
        }
    }
}