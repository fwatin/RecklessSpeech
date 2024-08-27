using Newtonsoft.Json;
using RecklessSpeech.Application.Read.Queries.Notes.Services;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Notes;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Infrastructure.Sequences.Gateways.Anki.FindNotes;
using RecklessSpeech.Infrastructure.Sequences.Gateways.Anki.UpdateNotes;
using System.Net.Http.Json;
using System.Text;
using Answer = RecklessSpeech.Domain.Sequences.Notes.Answer;
using Question = RecklessSpeech.Domain.Sequences.Notes.Question;

namespace RecklessSpeech.Infrastructure.Sequences.Gateways.Anki
{
    public class HttpAnkiNoteGateway : INoteGateway, IReadNoteGateway
    {
        private readonly HttpClient client;

        public HttpAnkiNoteGateway(HttpClient client) => this.client = client;

        public async Task Send(NoteDto note)
        {
            AnkiConnectAddNotesPayload pack = BuildPack(note);

            string? json = JsonConvert.SerializeObject(pack);

            StringContent stringContent = new(json, Encoding.UTF8, "application/json");
            HttpResponseMessage responseMessage;
            try
            {
                responseMessage = await this.client.PostAsync("", stringContent);
            }
            catch (Exception e)
            {
                throw new AnkiSendFailedException(
                    $"Exception thrown during post to Anki. Exception message : {e.Message} and Exception inner-exception : {e.InnerException?.Message}");
            }

            if (responseMessage.IsSuccessStatusCode is false)
            {
                throw new AnkiSendFailedException(
                    $"Status code is {responseMessage.StatusCode}. Reason phrase : {responseMessage.ReasonPhrase}");
            }

            string response = await responseMessage.Content.ReadAsStringAsync();

            AnkiConnectAddNotesResponse? ankiConnectResponse;
            try
            {
                ankiConnectResponse =
                    JsonConvert.DeserializeObject<AnkiConnectAddNotesResponse>(response);
            }
            catch (Exception e)
            {
                throw new AnkiSendFailedException(
                    $"fail while deserializing response into AnkiConnectAddNotesResponse. Json response is {response} and error message : {e.Message}");
            }

            if (string.IsNullOrEmpty(ankiConnectResponse.error) is false)
            {
                throw new AnkiSendFailedException($"Response specifies Anki error: {ankiConnectResponse.error}");
            }
        }

        public async Task AddTag(Domain.Sequences.Notes.Note note, string reversed)
        {
            AddTagPayload payload = new(note.AnkiId!.Value, reversed);
            StringContent findNotesIdsStringContent =
                new(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            var findIdsResponseMessage = await this.client.PostAsync("", findNotesIdsStringContent);
            var response = await findIdsResponseMessage.Content.ReadAsStringAsync();
        }

        private static AnkiConnectAddNotesPayload BuildPack(NoteDto dto)
        {
            Note note = new Note
            {
                deckName = "All::Langues",
                modelName = "Full_Recto_verso_before_after_Audio",
                options = new()
                {
                    allowDuplicate = true,
                    duplicateScope = "deck",
                    duplicateScopeOptions = new() { deckName = "All", checkChildren = false }
                },
                fields = CreateFields(dto),
                tags = CreateTags(dto)
            };

            AnkiConnectAddNotesPayload pack = new()
            {
                action = "addNotes", version = 6, @params = new() { notes = new[] { note } }
            };


            return pack;
        }

        private static string[] CreateTags(NoteDto dto)
        {
            return dto.Tags.Select(x => x.Value).ToArray();
        }

        private static Fields CreateFields(NoteDto dto) =>
            new()
            {
                Question = dto.Question.Value,
                Answer = dto.Answer.Value,
                After = dto.After.Value,
                Source = dto.Source.Value,
                Audio = dto.Audio.Value
            };

        public async Task<IReadOnlyCollection<Domain.Sequences.Notes.Note>> GetByFlagAndWithoutTag(int flagNumber,string missingTag)
        {
            FindAnkiNotesPayload payload = new($"flag:{flagNumber} -tag:{missingTag}");
            StringContent findNotesIdsStringContent =
                new(JsonConvert.SerializeObject(payload), Encoding.UTF8, "application/json");

            var findIdsResponseMessage = await this.client.PostAsync("", findNotesIdsStringContent);
            FindAnkiNotesResult? response =
                await findIdsResponseMessage.Content.ReadFromJsonAsync<FindAnkiNotesResult>();

            if (findIdsResponseMessage.IsSuccessStatusCode is false || response is null)
            {
                throw new("call to Anki failed for getting notes");
            }

            GetNotesInfoPayload getNotesInfo = new GetNotesInfoPayload(response.result);
            StringContent getNotesInfoStringContent =
                new(JsonConvert.SerializeObject(getNotesInfo), Encoding.UTF8, "application/json");
            var getNoteInfosMessage = await this.client.PostAsync("", getNotesInfoStringContent);

            GetNoteInfosResult? notesInfos = await getNoteInfosMessage.Content.ReadFromJsonAsync<GetNoteInfosResult>();

            if (notesInfos is null)
            {
                throw new("call to Anki failed for getting notes details");
            }

            return notesInfos.result.Select(AnkiResultToDomain).ToList();
        }

        private static RecklessSpeech.Domain.Sequences.Notes.Note AnkiResultToDomain(GetNoteInfoResult result)
        {
            Question question = Question.Create(HtmlContent.Hydrate(result.fields.Question.value));
            Answer answer = Answer.Create(result.fields.Answer.value);
            RecklessSpeech.Domain.Sequences.Notes.After after =
                Domain.Sequences.Notes.After.Create(result.fields.After.value);
            RecklessSpeech.Domain.Sequences.Notes.Source source =
                Domain.Sequences.Notes.Source.Create(result.fields.Source.value);
            RecklessSpeech.Domain.Sequences.Notes.Audio audio =
                Domain.Sequences.Notes.Audio.Create(result.fields.Audio.value);
            List<Tag> tags = result.tags.Select(Tag.Hydrate).ToList();

            var newNote =
                RecklessSpeech.Domain.Sequences.Notes.Note.Hydrate(
                    new(Guid.Empty), question, answer, after, source,
                    audio, tags);
            newNote.AnkiId = result.noteId;
            return newNote;
        }
    }
}