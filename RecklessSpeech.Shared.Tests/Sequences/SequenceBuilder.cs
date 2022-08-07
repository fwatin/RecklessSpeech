using RecklessSpeech.Application.Read.Queries.Sequences.GetAll;
using RecklessSpeech.Application.Write.Sequences.Commands;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Web.ViewModels.Sequences;

namespace RecklessSpeech.Shared.Tests.Sequences;

public record SequenceBuilder
{
    public SequenceId SequenceId { get; init; }
    public HtmlContentBuilder HtmlContent { get; init; }
    public AudioFileNameWithExtensionBuilder AudioFileNameWithExtension { get; init; }
    public TagsBuilder Tags { get; init; }


    private SequenceBuilder(
        SequenceIdBuilder sequenceId,
        HtmlContentBuilder htmlContent,
        AudioFileNameWithExtensionBuilder audioFileNameWithExtension,
        TagsBuilder tags)
    {
        this.SequenceId = sequenceId;
        this.HtmlContent = htmlContent;
        this.AudioFileNameWithExtension = audioFileNameWithExtension;
        this.Tags = tags;
    }


    public SequencesImportRequestedEvent BuildEvent() =>
        new(this.SequenceId, this.HtmlContent, this.AudioFileNameWithExtension, this.Tags);

    public static SequenceBuilder Create(Guid id)
    {
        return new SequenceBuilder(
            new(id),
            new(),
            new(),
            new());
    }

    public string BuildUnformatedSequence()
    {
        return $"\"{this.HtmlContent.Value}\"	[sound:{this.AudioFileNameWithExtension.Value}]	{this.Tags.Value}";
    }

    public SequenceEntity BuildEntity()
    {
        return new()
        {
            Id = this.SequenceId.Value,
            AudioFileNameWithExtension = this.AudioFileNameWithExtension.Value,
            Tags = this.Tags.Value,
            HtmlContent = this.HtmlContent.Value
        };
    }

    public SequenceSummaryQueryModel BuildQueryModel()
    {
        return new SequenceSummaryQueryModel(
            this.SequenceId.Value,
            this.HtmlContent.Value,
            this.AudioFileNameWithExtension.Value,
            this.Tags.Value);
    }

    public SequenceSummaryPresentation BuildSummaryPresentation()
    {
        return new SequenceSummaryPresentation(
            this.SequenceId.Value,
            this.HtmlContent.Value,
            this.AudioFileNameWithExtension.Value,
            this.Tags.Value);
    }

    public ImportSequencesCommand BuildImportCommand()
    {
        return new ImportSequencesCommand(this.BuildUnformatedSequence());
    }
}