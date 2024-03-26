using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Sequences.Sequences;
using System.Text;

namespace RecklessSpeech.Domain.Sequences.Notes
{
    public sealed class Note
    {
        private readonly After after;
        private readonly Answer? answer;
        private readonly Audio audio;
        private readonly Question question;
        private readonly Source source;
        private readonly Tags tags;

        private Note(NoteId id, Question question, Answer? answer, After after, Source source, Audio audio, Tags tags)
        {
            this.Id = id;
            this.question = question;
            this.answer = answer;
            this.after = after;
            this.source = source;
            this.audio = audio;
            this.tags = tags;
        }

        public NoteId Id { get; }

        public static Note Hydrate(NoteId id, Question question, Answer? answer, After after, Source source,
            Audio audio, Tags tags) =>
            new(id, question, answer, after, source, audio,tags);

        public static Note CreateFromSequence(Sequence sequence) =>
            new(
                new(Guid.NewGuid()),
                Question.Create(sequence.HtmlContent),
                CreateAnswer(sequence),
                CreateAfter(sequence),
                CreateSource(sequence),
                CreateAudio(sequence),
                CreateTags(sequence)
            );

        private static Answer CreateAnswer(Sequence sequence) => sequence.TranslatedWord != null
            ? Answer.Create(sequence.TranslatedWord!.Value)
            : Answer.Create("");

        private static Source CreateSource(Sequence sequence)
        {
            if (sequence.Explanations.Count == 0)
            {
                return Source.Create("");
            }

            StringBuilder urlBuilder = new();

            foreach (Explanation explanation in sequence.Explanations)
            {
                var url = explanation.SourceUrl.Value;
                string urlWithHyperlink = $"<a href=\"{url}\">{url}</a>";
                urlBuilder.AppendLine(urlWithHyperlink);
            }

            return Source.Create(urlBuilder.ToString());
        }

        private static Tags CreateTags(Sequence sequence)
        {
            if (sequence.Explanations.Any())
            {
                Language langue = sequence.Explanations.First().Language;
                if (langue is Dutch) return new Tags("Netflix_Neerlandais");
                if (langue is English) return new Tags("Netflix_Anglais");
                if (langue is Italian) return new Tags("Netflix_italien");
            }

            return new Tags("");
        }

        private static Audio CreateAudio(Sequence sequence)
        {
            if (string.IsNullOrEmpty(sequence.AudioFile.Value))
            {
                return new("");
            }

            string url = $"[sound:{sequence.AudioFile.Value}]";
            return Audio.Create(url);
        }

        private static After CreateAfter(Sequence sequence)
        {
            StringBuilder stringBuilder = new();

            stringBuilder.Append($"translated sentence from Netflix: \"{sequence.SentenceTranslations.GetMainSentenceTranslation()}\"");


            foreach (var explanation in sequence.Explanations)         
            {
                stringBuilder.AppendLine(explanation.Content.Value);
            }

            return After.Create(stringBuilder.ToString());
        }

        public NoteDto GetDto() => new(this.question, this.answer ?? new(""), this.after, this.source, this.audio,this.tags);
    }
}