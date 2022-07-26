using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Domain.Sequences;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Infrastructure.Entities;

namespace RecklessSpeech.Shared.Tests.Sequences;

public record SequenceBuilder(
    HtmlContent HtmlContent,
    AudioFileNameWithExtension AudioFileNameWithExtension,
    Tags Tags)
{
    public SequencesImportRequestedEvent BuildEvent() =>
        new(this.HtmlContent, this.AudioFileNameWithExtension, this.Tags);

    public static SequenceBuilder Create()
    {
        return new SequenceBuilder(HtmlContent.Hydrate(Some.SomeHtml),
            AudioFileNameWithExtension.Hydrate(Some.SomeAudiofileNameWithExtension),
            Tags.Hydrate(Some.SomeTags));
    }

    public string BuildUnformatedSequence()
    {
        return $"{this.HtmlContent.Value}	[sound:{this.AudioFileNameWithExtension.Value}]	{this.Tags.Value}";
    }

    public SequenceEntity BuildEntity()
    {
        return new(this.HtmlContent.Value, this.AudioFileNameWithExtension.Value, this.Tags.Value);
    }

    public SequenceQueryModel BuildQueryModel()
    {
        return new SequenceQueryModel(this.HtmlContent.Value, this.AudioFileNameWithExtension.Value, this.Tags.Value);
    }
}