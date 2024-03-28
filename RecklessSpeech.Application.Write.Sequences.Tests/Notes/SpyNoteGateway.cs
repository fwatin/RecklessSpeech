using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Notes
{
    public class SpyNoteGateway : INoteGateway {
        public NoteDto? Note { get; private set; }

        public async Task Send(NoteDto note)
        {
            this.Note = note;
            await Task.CompletedTask;
        }

        public Task AddTag(Note note, string reversed) => Task.CompletedTask;
    }
}