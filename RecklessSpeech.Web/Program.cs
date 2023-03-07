using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace RecklessSpeech.Web
{
    public static class Program
    {
        public static void Main(string[] args) =>
            CreateHostBuilder(args)
                .ConfigureHostConfiguration(config => config.AddEnvironmentVariables())
                .Build()
                .Run();

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => webBuilder.UseStartup<Startup>());
    }
}