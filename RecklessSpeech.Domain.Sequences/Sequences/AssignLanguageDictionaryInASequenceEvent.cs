using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Sequences;

public record AssignLanguageDictionaryInASequenceEvent(SequenceId SequenceId, LanguageDictionaryId LanguageDictionaryId) : IDomainEvent;