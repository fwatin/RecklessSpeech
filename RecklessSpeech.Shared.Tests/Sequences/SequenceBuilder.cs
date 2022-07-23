using System.Runtime.CompilerServices;
using RecklessSpeech.Domain.Sequences;
using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Shared.Tests.Sequences;

public record SequenceBuilder(
    string HtmlContent,
    AudioFileNameWithExtension AudioFileNameWithExtension,
    string Tags)
{
    public SequencesImportRequestedEvent BuildEvent() =>
        new(this.HtmlContent, this.AudioFileNameWithExtension, this.Tags);

    public static SequenceBuilder Create()
    {
        return new SequenceBuilder(Some.SomeHtml, AudioFileNameWithExtension.Hydrate(Some.SomeAudiofileNameWithExtension),
            Some.SomeTags);
    }

    public string BuildUnformatedSequence()
    {
        return $"{Some.SomeHtml}	[sound:{Some.SomeAudiofileNameWithExtension}]	{Some.SomeTags}";
    }

    public SequenceEntity BuildEntity()
    {
        return new(this.HtmlContent, this.AudioFileNameWithExtension.Value, this.Tags);
    }
}