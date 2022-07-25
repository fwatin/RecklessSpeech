using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace RecklessSpeech.Web.Configuration.Swagger
{
    public class ConfigureSwagger : IConfigureOptions<SwaggerGenOptions>, IConfigureOptions<SwaggerUIOptions>
    {
        private readonly IApiVersionDescriptionProvider provider;

        public ConfigureSwagger(IApiVersionDescriptionProvider provider)
        {
            this.provider = provider;
        }

        public void Configure(SwaggerGenOptions options)
        {
            options.CustomSchemaIds(x => x.FullName);

            this.provider.ApiVersionDescriptions.ToList().ForEach(description =>
                options.SwaggerDoc(
                    description.GroupName,
                    new OpenApiInfo
                    {
                        Title = "RecklessSpeech Api " + description.GroupName.ToUpperInvariant(),
                        Version = description.ApiVersion.ToString()
                    }));
        }

        public void Configure(SwaggerUIOptions options)
        {
            this.provider.ApiVersionDescriptions.ToList().ForEach(description =>
                options.SwaggerEndpoint(
                    $"/swagger/{description.GroupName}/swagger.json",
                    "RecklessSpeech Api " + description.GroupName.ToUpperInvariant()));
        }
    }
}