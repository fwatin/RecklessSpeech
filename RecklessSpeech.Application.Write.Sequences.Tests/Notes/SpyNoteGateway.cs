using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Notes
{
    public class SpyNoteGateway : INoteGateway
    {
        public SpyNoteGateway() => this.Notes = new();

        public List<NoteDto> Notes { get; }

        public async Task Send(IReadOnlyCollection<NoteDto> notes)
        {
            this.Notes.AddRange(notes);
            await Task.CompletedTask;
        }
    }
}