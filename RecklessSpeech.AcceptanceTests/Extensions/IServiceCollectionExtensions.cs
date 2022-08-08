using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Tests.Notes;

namespace RecklessSpeech.AcceptanceTests.Extensions;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection SubstituteNoteGateway(this IServiceCollection services)
    {
        SpyNoteGateway spyNoteGateway = new();

        return services
            .AddSingleton(spyNoteGateway)
            .AddSingleton<INoteGateway>(spyNoteGateway);
    }
}