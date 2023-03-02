using System.Text;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Domain.Sequences.Notes;

public sealed class Note
{
    public NoteId Id { get; }
    private readonly Question question;
    private readonly After after;
    private readonly Source source;
    private readonly Audio audio;

    private Note(NoteId id, Question question, After after, Source source, Audio audio)
    {
        this.Id = id;
        this.question = question;
        this.after = after;
        this.source = source;
        this.audio = audio;
    }

    public static Note Hydrate(NoteId id, Question question, After after, Source source, Audio audio) =>
        new(id, question, after, source, audio);

    public static Note CreateFromSequence(Sequence sequence)
    {
        return new Note(
            new(Guid.NewGuid()),
            Question.Create(sequence!.HtmlContent),
            CreateAfter(sequence),
            CreateSource(sequence),
            CreateAudio(sequence)
        );
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
        return new NoteDto(this.question, this.after, this.source, this.audio);
    }
}