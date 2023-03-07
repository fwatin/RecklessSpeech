using RecklessSpeech.Application.Core.Commands;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.AddDetails
{
    public record AddDetailsToSequencesCommand(SequenceDetailsDto dto) : IEventDrivenCommand;
}
