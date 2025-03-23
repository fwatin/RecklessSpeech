using Newtonsoft.Json;
using RecklessSpeech.Application.Write.Questioner.Ports;
using RecklessSpeech.Domain.Questioner;
using RecklessSpeech.Infrastructure.Questioner.FindNotes;
using System.Net.Http.Json;
using System.Text;
using After = RecklessSpeech.Domain.Questioner.After;
using Answer = RecklessSpeech.Domain.Questioner.Answer;
using Question = RecklessSpeech.Domain.Questioner.Question;

namespace RecklessSpeech.Infrastructure.Questioner
{
    public class HttpAnkiNoteGateway(HttpClient client) : IReadNoteGateway
    {
        public async Task<IReadOnlyCollection<Note>> GetBySubject(string subject)
        {
            FindAnkiNotesPayload payload = new($"question:*{subject}* source:*chatgpt*");
            StringContent findNotesIdsStringContent =
                new(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            var findIdsResponseMessage = await client.PostAsync("", findNotesIdsStringContent);
            FindAnkiNotesResult? response =
                await findIdsResponseMessage.Content.ReadFromJsonAsync<FindAnkiNotesResult>();

            if (findIdsResponseMessage.IsSuccessStatusCode is false || response is null)
            {
                throw new("call to Anki failed for getting notes");
            }

            GetNotesInfoPayload getNotesInfo = new GetNotesInfoPayload(response.result);
            StringContent getNotesInfoStringContent =
                new(JsonConvert.SerializeObject(getNotesInfo), Encoding.UTF8, "application/json");
            var getNoteInfosMessage = await client.PostAsync("", getNotesInfoStringContent);

            GetNoteInfosResult? notesInfos = await getNoteInfosMessage.Content.ReadFromJsonAsync<GetNoteInfosResult>();

            if (notesInfos is null)
            {
                throw new("call to Anki failed for getting notes details");
            }

            return notesInfos.result.Select(AnkiResultToDomain).ToList();
        }
        
        private static Note AnkiResultToDomain(GetNoteInfoResult result)
        {
            Question question = Question.Create((result.fields.Question.value));
            Answer answer = Answer.Create(result.fields.Answer.value);
            After after = After.Create(result.fields.After.value);
            var newNote = Note.Hydrate(new(Guid.NewGuid()), question, answer, after);
            newNote.AnkiId = result.noteId;
            return newNote;
        }
    }
}