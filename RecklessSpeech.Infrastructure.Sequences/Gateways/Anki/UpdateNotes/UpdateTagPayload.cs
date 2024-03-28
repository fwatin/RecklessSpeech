namespace RecklessSpeech.Infrastructure.Sequences.Gateways.Anki.UpdateNotes
{

    public class AddTagPayload
    {
        public string action = "addTags";
        public int version = 6;
        public AddTagParams @params { get; set; }

        public AddTagPayload(long noteId, string tag)
        {
            this.@params = new() { notes = new[] { noteId }, tags = tag };
        }
    }

    public class AddTagParams
    {
        public long[] notes { get; set; }
        public string tags { get; set; }
    }


}