using System.Text;
using Newtonsoft.Json;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Notes;

namespace RecklessSpeech.Infrastructure.Sequences.AnkiGateway;

public class HttpAnkiNoteGateway : INoteGateway
{
    private readonly HttpClient client;

    public HttpAnkiNoteGateway(HttpClient client)
    {
        this.client = client;
    }

    public async Task Send(IReadOnlyCollection<NoteDto> notes)
    {
        AnkiConnectAddNotesPayload pack = BuildPack(notes);

        var json = JsonConvert.SerializeObject(pack);

        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage responseMessage = await client.PostAsync("", stringContent);

        if (responseMessage.IsSuccessStatusCode is false) throw new AnkiSendFailedException();

        var response = await responseMessage.Content.ReadAsStringAsync();
        
        var ankiConnectResponse = JsonConvert.DeserializeObject<AnkiConnectAddNotesResponse>(response);

    }

    private AnkiConnectAddNotesPayload BuildPack(IReadOnlyCollection<NoteDto> dtos)
    {
        var pack = new AnkiConnectAddNotesPayload
        {
            action = "addNotes",
            version = 6,
            @params = new Params()
        };


        List<Note> notes = new();
        foreach (var dto in dtos)
        {
            notes.Add(new Note()
            {
                deckName = "All::Langues",
                modelName = "Full_Recto_verso_before_after_Audio",
                options = new options()
                {
                    allowDuplicate = true,
                    duplicateScope = "deck",
                    duplicateScopeOptions = new duplicateScopeOptions()
                    {
                        deckName = "All",
                        checkChildren = false,
                    }
                },
                fields = new Fields()
                {
                    Question = dto.Question.Value
                }
            });
        }

        pack.@params.notes = notes.ToArray();
        return pack;
    }
}