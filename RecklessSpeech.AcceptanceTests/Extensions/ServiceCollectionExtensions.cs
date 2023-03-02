using Microsoft.Extensions.DependencyInjection;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;
using RecklessSpeech.Application.Write.Sequences.Tests.Notes;
using RecklessSpeech.Infrastructure.Sequences.TranslatorGateways.Mijnwoordenboek;

namespace RecklessSpeech.AcceptanceTests.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection SubstituteNoteGateway(this IServiceCollection services)
    {
        SpyNoteGateway spyNoteGateway = new();

        return services
            .AddSingleton(spyNoteGateway)
            .AddSingleton<INoteGateway>(spyNoteGateway);
    }
    
    public static IServiceCollection SubstituteMijnwoordenboekGatewayAccess(this IServiceCollection services)
    {
        MijnwoordenboekGatewayLocalAccess access = new();

        return services
            .AddSingleton(access)
            .AddSingleton<IMijnwoordenboekGatewayAccess>(access);
    }
}