using HtmlAgilityPack;
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
        private readonly List<Tag> tags;

        private Note(NoteId id, Question question, Answer? answer, After after, Source source, Audio audio,
            List<Tag> tags)
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
        public long? AnkiId { get; set; }

        public static Note Hydrate(NoteId id, Question question, Answer? answer, After after, Source source,
            Audio audio, List<Tag> tags) =>
            new(id, question, answer, after, source, audio, tags);

        public static Note CreateFromSequence(Sequence sequence) =>
            new(
                new(Guid.NewGuid()),
                Question.Create(sequence.HtmlContent),
                CreateAnswer(sequence),
                CreateAfter(sequence),
                CreateSource(sequence),
                CreateAudio(sequence),
                CreateTags(sequence).ToList()
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

        private static Tag[] CreateTags(Sequence sequence)
        {
            if (sequence.Explanations.Any())
            {
                Language langue = sequence.Explanations.First().Language;
                switch (langue)
                {
                    case Dutch:
                        return new Tag[1] { new("Netflix_Neerlandais") };
                    case English:
                        return new Tag[1] { new("Netflix_Anglais") };
                    case Italian:
                        return new Tag[1] { new("Netflix_Italien") };
                }
            }

            return Array.Empty<Tag>();
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

            stringBuilder.Append(
                $"translated sentence from Netflix: \"{sequence.SentenceTranslations.GetMainSentenceTranslation()}\"");


            foreach (var explanation in sequence.Explanations)
            {
                stringBuilder.AppendLine(explanation.Content.Value);
            }

            return After.Create(stringBuilder.ToString());
        }

        public NoteDto GetDto() =>
            new(this.question, this.answer ?? new(""), this.after, this.source, this.audio, this.tags);

        public Note CreateReversedNote()
        {
            var treatHtml = this.GetWordInRedAndHtmlWithDot(this.question.Value);
            Question reversedQuestion = Question.Create(new(treatHtml.Item2));
            Answer reversedAnswer = Answer.Create(treatHtml.Item1);
            List<Tag> reversedTags = new(this.tags) { new("result_from_inversion") };

            return new(
                new(Guid.NewGuid()),
                reversedQuestion,
                reversedAnswer,
                this.after,
                this.source,
                this.audio,
                reversedTags);
        }

        private (string, string) GetWordInRedAndHtmlWithDot(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);
            string wordInRed = "";

            var spanNodes = doc.DocumentNode.SelectNodes("//span[@style]");
            if (spanNodes != null)
            {
                bool aTargetHasAlreadyBeenFound = false;
                int lasWordInRed = 0;

                for (int i = 0; i < spanNodes.Count; i++)
                {
                    var span = spanNodes[i];

                    if (IsATarget(span))
                    {
                        if (aTargetHasAlreadyBeenFound is false)
                        {
                            wordInRed = span.InnerText;
                            span.InnerHtml = "[traduire le mot : " + this.answer!.Value + "]";
                            span.SetAttributeValue("style", "background-color: rgb(0, 170, 0);");
                            aTargetHasAlreadyBeenFound = true;
                            lasWordInRed = i;
                        }
                        else
                        {
                            wordInRed += span.InnerText;
                            if (i == lasWordInRed + 1)
                            {
                                span.InnerHtml = "";
                                span.SetAttributeValue("style", "background-color: rgb(0, 170, 0);");
                            }
                            else throw new("cas non géré");
                        }
                    }
                }
            }

            var newHtml = doc.DocumentNode.OuterHtml;

            return (wordInRed, newHtml);
        }

        public bool IsATarget(HtmlNode span)
        {
            var style = span.GetAttributeValue("style", "");
            return style.Contains("background-color: rgb(157, 0, 0);") ||
                   style.Contains("background-color: brown;");
        }

        public bool IsAlreadyReversed()
        {
            return this.tags.Any(w => w.Value == "reversed");
        }
    }
}