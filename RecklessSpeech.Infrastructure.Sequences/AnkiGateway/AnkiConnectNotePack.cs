namespace RecklessSpeech.Infrastructure.Sequences.AnkiGateway;

public class AnkiConnectNotePack
{
    public string action { get; set; }
    public int version { get; set; }
    public Params _params { get; set; }
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
    public string[] tags { get; set; }
    public Audio[] audio { get; set; }
    public Video[] video { get; set; }
    public Picture[] picture { get; set; }
}

public class Fields
{
    public string Question { get; set; }
    public string Answer { get; set; }
    public string After { get; set; }
    public string Source { get; set; }
    public string Audio { get; set; }
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