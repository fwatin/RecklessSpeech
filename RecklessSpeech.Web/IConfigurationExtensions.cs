using Microsoft.Extensions.Configuration;

namespace RecklessSpeech.Web
{
    public static class IConfigurationExtensions
    {
        private const string ShouldUseSwaggerKey = "SWAGGER_IS_ACTIVE";

        public static bool IsSwaggerActive(this IConfiguration configuration) =>
            configuration.GetSection(ShouldUseSwaggerKey).Value == "true";
    }
}