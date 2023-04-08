using Newtonsoft.Json;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Notes;
using System.Text;

namespace RecklessSpeech.Infrastructure.Sequences.Gateways.Anki
{
    public class HttpAnkiNoteGateway : INoteGateway
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
                fields = CreateFields(dto)
            };

            AnkiConnectAddNotesPayload pack = new()
            {
                action = "addNotes", version = 6, @params = new() { notes = new[] { note } }
            };


            return pack;
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
    }
}