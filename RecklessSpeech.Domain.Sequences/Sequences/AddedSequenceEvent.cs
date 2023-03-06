using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Sequences;

public record AddedSequenceEvent
(
    SequenceId Id,
    HtmlContent HtmlContent,
    AudioFileNameWithExtension AudioFileNameWithExtension,
    Tags Tags,
    Word Word,
    TranslatedSentence TranslatedSentence,
    TranslatedWord? TranslatedWord
) : IDomainEvent;