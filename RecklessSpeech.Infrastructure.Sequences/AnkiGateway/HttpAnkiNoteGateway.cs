using Newtonsoft.Json;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Notes;
using System.Text;

namespace RecklessSpeech.Infrastructure.Sequences.AnkiGateway
{
    public class HttpAnkiNoteGateway : INoteGateway
    {
        private readonly HttpClient client;

        public HttpAnkiNoteGateway(HttpClient client) => this.client = client;

        public async Task
            Send(IReadOnlyCollection<NoteDto> notes) //todo clean passer sur un seul send à la fois et throw exception si ca foire
        {
            AnkiConnectAddNotesPayload pack = BuildPack(notes);

            string? json = JsonConvert.SerializeObject(pack);

            StringContent stringContent = new(json, Encoding.UTF8, "application/json");

            HttpResponseMessage responseMessage = await this.client.PostAsync("", stringContent);

            if (responseMessage.IsSuccessStatusCode is false)
            {
                throw new AnkiSendFailedException();
            }

            string response = await responseMessage.Content.ReadAsStringAsync();

            AnkiConnectAddNotesResponse? ankiConnectResponse =
                JsonConvert.DeserializeObject<AnkiConnectAddNotesResponse>(response);
        }

        private static AnkiConnectAddNotesPayload BuildPack(IEnumerable<NoteDto> dtos)
        {
            AnkiConnectAddNotesPayload pack = new()
            {
                action = "addNotes",
                version = 6,
                @params = new()
                {
                    notes = dtos.Select(dto => new Note
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
                    }).ToArray()
                }
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