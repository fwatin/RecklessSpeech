using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace RecklessSpeech.Web.Configuration
{
    public class ConfigureMvcOptions : IConfigureOptions<JsonOptions>
    {
        public void Configure(JsonOptions options)
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        }
    }
}