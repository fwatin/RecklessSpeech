namespace RecklessSpeech.Infrastructure.Sequences.Gateways.Anki.FindNotes
{
    public class GetNoteInfosResult
    {
        public GetNoteInfoResult[] result { get; set; }
        public object error { get; set; }
    }

    public class GetNoteInfoResult
    {
        public long noteId { get; set; }
        public string[] tags { get; set; }
        public Fields fields { get; set; }
        public string modelName { get; set; }
        public long[] cards { get; set; }
    }

    public class Fields
    {
        public Question Question { get; set; }
        public Answer Answer { get; set; }
        public Before Before { get; set; }
        public After After { get; set; }
        public Source Source { get; set; }
        public Audio Audio { get; set; }
        public Mem_Image Mem_Image { get; set; }
        public Mem_Text Mem_Text { get; set; }
        public AddReverseQuestion AddreverseQuestion { get; set; }
        public AddReverseAnswer AddReverseAnswer { get; set; }
    }

    public class Question
    {
        public string value { get; set; }
        public int order { get; set; }
    }

    public class Answer
    {
        public string value { get; set; }
        public int order { get; set; }
    }

    public class Before
    {
        public string value { get; set; }
        public int order { get; set; }
    }

    public class After
    {
        public string value { get; set; }
        public int order { get; set; }
    }

    public class Source
    {
        public string value { get; set; }
        public int order { get; set; }
    }

    public class Audio
    {
        public string value { get; set; }
        public int order { get; set; }
    }

    public class Mem_Image
    {
        public string value { get; set; }
        public int order { get; set; }
    }

    public class Mem_Text
    {
        public string value { get; set; }
        public int order { get; set; }
    }

    public class AddReverseQuestion
    {
        public string value { get; set; }
        public int order { get; set; }
    }

    public class AddReverseAnswer
    {
        public string value { get; set; }
        public int order { get; set; }
    }
}