using RecklessSpeech.Application.Core.Events;

namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public record SetTranslatedWordEvent(SequenceId SequenceId, TranslatedWord TranslatedWord) : IDomainEvent;
}