using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public record SetTranslatedWordEvent(SequenceId Id, TranslatedWord TranslatedWord) : IDomainEvent;
}