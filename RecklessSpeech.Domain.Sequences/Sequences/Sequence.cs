using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Sequences;

public sealed class Sequence//todo inherite from aggregateId?
{
    private readonly Guid sequenceId;
    private HtmlContent htmlContent = default!;
    private AudioFileNameWithExtension audioFile= default!;
    private Tags tags= default!;

    //todo value type for sequence id
    private Sequence(Guid sequenceId)
    {
        this.sequenceId = sequenceId;
    }

    public IEnumerable<IDomainEvent> Import()
    {
        yield return new SequencesImportRequestedEvent(
            this.sequenceId,
            this.htmlContent,
            this.audioFile,
            this.tags);
    }

    public static Sequence Hydrate(Guid id) => new(id);

    public static Sequence Create(
        Guid id,
        HtmlContent htmlContent, 
        AudioFileNameWithExtension audioFileNameWithExtension, 
        Tags tags)
    {
        return new Sequence(id)
        {
            htmlContent = htmlContent,
            audioFile = audioFileNameWithExtension,
            tags = tags
        };
    }
}