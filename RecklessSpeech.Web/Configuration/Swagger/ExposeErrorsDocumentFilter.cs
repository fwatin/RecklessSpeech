using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace RecklessSpeech.Web.Configuration.Swagger
{
    public class ExposeErrorsDocumentFilter : IDocumentFilter
    {
        public void Apply(OpenApiDocument swaggerDoc, DocumentFilterContext context)
        {
            context.SchemaGenerator.GenerateSchema(typeof(ApiErrors), context.SchemaRepository);
        }
    }
}