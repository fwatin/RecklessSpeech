namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public record OriginalSentences(List<string> Values)
    {
        public static OriginalSentences Create(List<string> value) => new(value);
        
        public string Joined()
        {
            return string.Join(" ", this.Values);
        }
        public string GetCentralSentence()
        {
            if (Values.Count == 1) return Values[0];
            return this.Values[1];
        }
    }
}