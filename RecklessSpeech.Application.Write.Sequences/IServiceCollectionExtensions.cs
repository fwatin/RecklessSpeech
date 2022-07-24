using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace RecklessSpeech.Application.Write.Sequences;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddSequencesCommands(this IServiceCollection services)
    {
        return services.AddMediatR(Assembly.GetExecutingAssembly());
    }
}