using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RecklessSpeech.Application.Write.Sequences
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddSequencesCommands(this IServiceCollection services) =>
            services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}