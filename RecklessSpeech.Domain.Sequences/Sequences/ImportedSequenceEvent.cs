using RecklessSpeech.Application.Core.Events;

namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public record ImportedSequenceEvent
    (
        SequenceId Id,
        HtmlContent HtmlContent,
        AudioFileNameWithExtension AudioFileNameWithExtension,
        Tags Tags,
        Word Word,
        TranslatedSentence TranslatedSentence,
        TranslatedWord? TranslatedWord,
        MediaId MediaId
    ) : IDomainEvent;
}