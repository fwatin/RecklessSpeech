using System.Text;
using HtmlAgilityPack;
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

        string? json = JsonConvert.SerializeObject(pack);

        StringContent? stringContent = new(json, Encoding.UTF8, "application/json");

        HttpResponseMessage responseMessage = await this.client.PostAsync("", stringContent);

        if (responseMessage.IsSuccessStatusCode is false) throw new AnkiSendFailedException();

        string? response = await responseMessage.Content.ReadAsStringAsync();
        
        AnkiConnectAddNotesResponse? ankiConnectResponse = JsonConvert.DeserializeObject<AnkiConnectAddNotesResponse>(response);

    }

    private static AnkiConnectAddNotesPayload BuildPack(IEnumerable<NoteDto> dtos)
    {
        AnkiConnectAddNotesPayload? pack = new()
        {
            action = "addNotes",
            version = 6,
            @params = new Params
            {
                notes = dtos.Select(dto => new Note()
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
                    fields = CreateFields(dto)
                }).ToArray()
            }
        };


        return pack;
    }

    private static Fields CreateFields(NoteDto dto)
    {
        if(dto.Answer is not null)
        {
            return new Fields()
            {
                Question = dto.Question.Value,
                Answer = dto.Answer.Value,
                After = dto.After.Value,
                Source = dto.Source.Value,
                Audio = dto.Audio.Value
            };
        }
        return new Fields()
        {
            Question = dto.Question.Value,
            Answer = "",
            After = dto.After.Value,
            Source = dto.Source.Value,
            Audio = dto.Audio.Value
        };
    }
}