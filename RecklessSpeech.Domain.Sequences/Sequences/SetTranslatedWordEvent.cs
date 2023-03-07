using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public record SetTranslatedWordEvent(SequenceId SequenceId, TranslatedWord TranslatedWord) : IDomainEvent;
}