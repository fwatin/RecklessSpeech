using RecklessSpeech.Application.Core.Commands;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Media
{
    public record SaveMediaCommand(string EntryFullName, byte[] Content) : IEventDrivenCommand;

}