namespace RecklessSpeech.Infrastructure.Sequences.Gateways.Anki.FindNotes
{
    public class GetNotesInfoPayload
    {
        public GetNotesInfoPayload(IReadOnlyCollection<long> noteIds)
        {
            this.@params = new() { notes = noteIds.ToArray() };
        }

        public string action = "notesInfo";
        public int version = 6;
        public GetNotesDetailParams @params { get; set; }
    }

    public class GetNotesDetailParams
    {
        public long[] notes { get; set; }
    }
}