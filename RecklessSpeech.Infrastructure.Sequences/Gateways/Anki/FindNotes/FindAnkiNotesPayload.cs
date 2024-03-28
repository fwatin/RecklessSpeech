namespace RecklessSpeech.Infrastructure.Sequences.Gateways.Anki.FindNotes
{
    public class FindAnkiNotesPayload
    {
        public FindAnkiNotesPayload(string query)
        {
            this.@params = new() { query = query };
        }

        public string action = "findNotes";
        public int version = 6;
        public Params @params { get; set; }
    }

    public class Params
    {
        public string query { get; set; }
    }
}