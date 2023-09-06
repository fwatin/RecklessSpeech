namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public class SentenceTranslations
    {
        private readonly string?[]? human;
        private readonly string?[]? machine;
        private SentenceTranslations(string?[]? human, string?[]? machine)
        {
            this.human = human;
            this.machine = machine;

        }

        public static SentenceTranslations Create(string?[]? human, string?[]? machine)
        {
            return new(human,machine);
        }
        
        public string GetMainSentenceTranslation()
        {
            if (this.human is not null)
            {
                if (this.human[1] is not null) return this.human[1]!;
            }
            else if (this.machine is not null)
            {
                if (this.machine[1] is not null) return this.machine[1]!;
            }

            return "No translation neither human or machine for middle sentence";
        }
    }

}