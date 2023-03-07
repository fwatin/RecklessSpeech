using System.Text;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Domain.Sequences.Notes;

public sealed class Note
{
    public NoteId Id { get; }
    private readonly Question question;
    private readonly Answer? answer;
    private readonly After after;
    private readonly Source source;
    private readonly Audio audio;

    private Note(NoteId id, Question question, Answer? answer, After after, Source source, Audio audio)
    {
        this.Id = id;
        this.question = question;
        this.answer = answer;
        this.after = after;
        this.source = source;
        this.audio = audio;
    }

    public static Note Hydrate(NoteId id, Question question, Answer? answer, After after, Source source, Audio audio) =>
        new(id, question, answer, after, source, audio);

    public static Note CreateFromSequence(Sequence sequence)
    {
        return new(
            new(Guid.NewGuid()),
            Question.Create(sequence!.HtmlContent),
            CreateAnswer(sequence),
            CreateAfter(sequence),
            CreateSource(sequence),
            CreateAudio(sequence)
        );
    }

    private static Answer? CreateAnswer(Sequence sequence)
    {
        return sequence.TranslatedWord != null ? Answer.Create(sequence.TranslatedWord!.Value) : Answer.Create("");
    }

    private static Source CreateSource(Sequence sequence)
    {
        if (sequence.Explanation is null) return Source.Create("");

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

    public NoteDto GetDto()
    {
        return new(this.question, this.answer, this.after, this.source, this.audio);
    }
}