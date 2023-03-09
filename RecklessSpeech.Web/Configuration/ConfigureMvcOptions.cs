using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Text.Json.Serialization;

namespace RecklessSpeech.Web.Configuration
{
    public class ConfigureMvcOptions : IConfigureOptions<JsonOptions>
    {
        public void Configure(JsonOptions options) =>
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }
}