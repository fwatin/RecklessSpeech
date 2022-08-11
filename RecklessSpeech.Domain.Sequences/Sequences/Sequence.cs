using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Sequences;

public sealed class Sequence
{
    private readonly SequenceId sequenceId;
    private HtmlContent htmlContent = default!;
    private Word word = default!;
    private AudioFileNameWithExtension audioFile= default!;
    private Tags tags= default!;

    private Sequence(SequenceId sequenceId)
    {
        this.sequenceId = sequenceId;
    }

    public IEnumerable<IDomainEvent> Import()
    {
        yield return new SequencesImportRequestedEvent(
            this.sequenceId,
            this.htmlContent,
            this.audioFile,
            this.tags,
            this.word);
    }

    public static Sequence Create(
        Guid id,
        HtmlContent htmlContent, 
        AudioFileNameWithExtension audioFileNameWithExtension, 
        Tags tags,
        Word word)
    {
        return new Sequence(new(id))
        {
            htmlContent = htmlContent,
            audioFile = audioFileNameWithExtension,
            tags = tags,
            word = word
        };
    }
}