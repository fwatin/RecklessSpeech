using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Sequences;

public record SetLanguageDictionaryInASequenceEvent(SequenceId SequenceId, LanguageDictionaryId LanguageDictionaryId) : IDomainEvent;