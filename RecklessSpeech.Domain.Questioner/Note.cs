using System.Text;

namespace RecklessSpeech.Domain.Questioner
{
    public sealed class Note
    {
        private readonly Question question;
        private readonly Answer? answer;
        private readonly After after;
        
        public string Slimmed => 
            $"Question: {question}" +
            "---" + 
            $"Réponse : {answer}";

        private Note(NoteId id, Question question, Answer? answer, After after)
        {
            this.Id = id;
            this.question = question;
            this.answer = answer;
            this.after = after;
        }

        public NoteId Id { get; }
        public long? AnkiId { get; set; }

        public static Note Hydrate(NoteId id, Question question, Answer? answer, After after) =>
            new(id, question, answer, after);
    }
}