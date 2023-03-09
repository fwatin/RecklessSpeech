﻿using RecklessSpeech.Domain.Sequences.Sequences;
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

        private Note(NoteId id, Question question, Answer? answer, After after, Source source, Audio audio)
        {
            this.Id = id;
            this.question = question;
            this.answer = answer;
            this.after = after;
            this.source = source;
            this.audio = audio;
        }

        public NoteId Id { get; }

        public static Note Hydrate(NoteId id, Question question, Answer? answer, After after, Source source,
            Audio audio) =>
            new(id, question, answer, after, source, audio);

        public static Note CreateFromSequence(Sequence sequence) =>
            new(
                new(Guid.NewGuid()),
                Question.Create(sequence.HtmlContent),
                CreateAnswer(sequence),
                CreateAfter(sequence),
                CreateSource(sequence),
                CreateAudio(sequence)
            );

        private static Answer CreateAnswer(Sequence sequence) => sequence.TranslatedWord != null
            ? Answer.Create(sequence.TranslatedWord!.Value)
            : Answer.Create("");

        private static Source CreateSource(Sequence sequence)
        {
            if (sequence.Explanation is null)
            {
                return Source.Create("");
            }

            string url = sequence.Explanation.SourceUrl.Value;

            string urlWithHyperlink = $"<a href=\"{url}\">{url}</a>";

            return Source.Create(urlWithHyperlink);
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

            stringBuilder.Append($"translated sentence from Netflix: \"{sequence.TranslatedSentence.Value}\"");

            if (sequence.Explanation is not null)
            {
                stringBuilder.AppendLine(sequence.Explanation.Content.Value);
            }

            return After.Create(stringBuilder.ToString());
        }

        public NoteDto GetDto() => new(this.question, this.answer ?? new(""), this.after, this.source, this.audio);
    }
}