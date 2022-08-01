using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Domain.Sequences;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Web.ViewModels.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences;

public record SequenceBuilder
{
    public HtmlContent HtmlContent { get; init; } //todo set builder
    public AudioFileNameWithExtension AudioFileNameWithExtension { get; init; }
    public Tags Tags { get; init; }
    public Guid Id { get; init; }


    private SequenceBuilder(
        Guid id,
        HtmlContent htmlContent,
        AudioFileNameWithExtension audioFileNameWithExtension,
        Tags tags)
    {
        this.Id = id;
        this.HtmlContent = htmlContent;
        this.AudioFileNameWithExtension = audioFileNameWithExtension;
        this.Tags = tags;
    }


    public SequencesImportRequestedEvent BuildEvent() =>
        new(this.Id, this.HtmlContent, this.AudioFileNameWithExtension, this.Tags);

    public static SequenceBuilder Create(Guid id)
    {
        return new SequenceBuilder(
            id,
            HtmlContent.Hydrate(Some.SomeHtml),
            AudioFileNameWithExtension.Hydrate(Some.SomeAudiofileNameWithExtension),
            Tags.Hydrate(Some.SomeTags));
    }

    public string BuildUnformatedSequence()
    {
        return $"{this.HtmlContent.Value}	[sound:{this.AudioFileNameWithExtension.Value}]	{this.Tags.Value}";
    }

    public SequenceEntity BuildEntity()
    {
        return new()
        {
            Id = this.Id,
            AudioFileNameWithExtension = this.AudioFileNameWithExtension.Value,
            Tags = this.Tags.Value,
            HtmlContent = this.HtmlContent.Value
        };
    }

    public SequenceSummaryQueryModel BuildQueryModel()
    {
        return new SequenceSummaryQueryModel(this.HtmlContent.Value, this.AudioFileNameWithExtension.Value,
            this.Tags.Value);
    }

    public SequenceSummaryPresentation BuildSummaryPresentation()
    {
        return new SequenceSummaryPresentation(this.HtmlContent.Value, this.AudioFileNameWithExtension.Value,
            this.Tags.Value);
    }
}