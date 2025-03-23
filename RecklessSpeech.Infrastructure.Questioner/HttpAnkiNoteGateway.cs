using RecklessSpeech.Application.Write.Questioner.Ports;
using RecklessSpeech.Domain.Questioner;
using System.Net.Http.Json;
using System.Text;

namespace RecklessSpeech.Infrastructure.Questioner
{
    public class HttpAnkiNoteGateway(HttpClient client) : IReadNoteGateway
    {
        public Task<IReadOnlyCollection<Note>> GetBySubject(string subject)
        {
            throw new NotImplementedException();
        }
    }
}