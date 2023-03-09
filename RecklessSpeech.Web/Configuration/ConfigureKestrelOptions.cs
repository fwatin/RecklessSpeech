using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Options;

namespace RecklessSpeech.Web.Configuration
{
    public class ConfigureKestrelOptions : IConfigureOptions<KestrelServerOptions>
    {
        public void Configure(KestrelServerOptions options) => options.AddServerHeader = false;
    }
}