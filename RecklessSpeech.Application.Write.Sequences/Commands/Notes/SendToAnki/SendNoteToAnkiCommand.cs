using MediatR;
using RecklessSpeech.Application.Core.Commands;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Notes.SendToAnki
{
    public record SendNoteToAnkiCommand(Guid Id)  : IRequest;
}