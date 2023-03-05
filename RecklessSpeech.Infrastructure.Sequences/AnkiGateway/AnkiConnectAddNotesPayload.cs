#pragma warning disable CS8618
namespace RecklessSpeech.Infrastructure.Sequences.AnkiGateway;

public class AnkiConnectAddNotesPayload
{
    public string action { get; set; }
    public int version { get; set; }
    public Params @params { get; set; }
}

public class Params
{
    public Note[] notes { get; set; }
}

public class Note
{
    public string deckName { get; set; }
    public string modelName { get; set; }
    public Fields fields { get; set; }

    public options options { get; set; }

    public Picture[] picture { get; set; }
}

public class Fields
{
    public string Question { get; set; } = default!;
    public string Answer { get; set; } = default!;
    public string After { get; set; } = default!;
    public string Source { get; set; } = default!;
    public string Audio { get; set; } = default!;
}

public class options
{
    public bool allowDuplicate { get; set; }
    public string duplicateScope { get; set; }
    public duplicateScopeOptions duplicateScopeOptions { get; set; }
}

public class duplicateScopeOptions
{
    public string deckName { get; set; }
    public bool checkChildren { get; set; }
}

public class Audio
{
    public string url { get; set; }
    public string filename { get; set; }
    public string skipHash { get; set; }
    public string[] fields { get; set; }
}

public class Video
{
    public string url { get; set; }
    public string filename { get; set; }
    public string skipHash { get; set; }
    public string[] fields { get; set; }
}

public class Picture
{
    public string url { get; set; }
    public string filename { get; set; }
    public string skipHash { get; set; }
    public string[] fields { get; set; }
}