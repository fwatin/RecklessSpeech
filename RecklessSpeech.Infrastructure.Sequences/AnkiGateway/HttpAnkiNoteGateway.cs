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
        client.BaseAddress = new Uri("http://localhost:8765/"); //todo mettre config dans appsettings
    }

    public async Task Send(IReadOnlyCollection<NoteDto> notes)
    {
        AnkiConnectNotePack pack = BuildPack(notes);

        var json = JsonConvert.SerializeObject(pack);

        var stringContent = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response = await client.PostAsync("", stringContent);

        if (response.IsSuccessStatusCode is false) throw new AnkiSendFailedException();
    }

    private AnkiConnectNotePack BuildPack(IReadOnlyCollection<NoteDto> dtos)
    {
        var pack = new AnkiConnectNotePack();
        pack.action = "addNote";
        pack.version = 6;
        pack._params = new Params();


        List<Note> notes = new();
        foreach (var dto in dtos)
        {
            notes.Add(new Note
            {
                deckName = "All",
                modelName = "Full_Recto_verso_before_after_Audio",
                fields = new Fields()
                {
                    Question = dto.Question.Value
                }
            });
        }

        pack._params.notes = notes.ToArray();
        return pack;
    }
}