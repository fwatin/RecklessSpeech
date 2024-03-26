// ReSharper disable All

#pragma warning disable CS8618
namespace RecklessSpeech.Infrastructure.Sequences.Gateways.Anki
{
    public class AnkiConnectAddNotesPayload
    {
        public string action { get; init; }
        public int version { get; init; }
        public Params @params { get; init; }
    }

    public class Params
    {
        public Note[] notes { get; init; }
    }

    public class Note
    {
        public string[] tags { get; init; }
        public string deckName { get; init; }
        public string modelName { get; init; }
        public Fields fields { get; init; }

        public options options { get; init; }

        public Picture[] picture { get; init; }
    }

    public class Fields
    {
        public string Question { get; init; } = default!;
        public string Answer { get; init; } = default!;
        public string After { get; init; } = default!;
        public string Source { get; init; } = default!;
        public string Audio { get; init; } = default!;
    }

    public class options
    {
        public bool allowDuplicate { get; init; }
        public string duplicateScope { get; init; }
        public duplicateScopeOptions duplicateScopeOptions { get; init; }
    }

    public class duplicateScopeOptions
    {
        public string deckName { get; init; }
        public bool checkChildren { get; init; }
    }

    public class Audio
    {
        public string url { get; init; }
        public string filename { get; init; }
        public string skipHash { get; init; }
        public string[] fields { get; init; }
    }

    public class Video
    {
        public string url { get; init; }
        public string filename { get; init; }
        public string skipHash { get; init; }
        public string[] fields { get; init; }
    }

    public class Picture
    {
        public string url { get; init; }
        public string filename { get; init; }
        public string skipHash { get; init; }
        public string[] fields { get; init; }
    }
}