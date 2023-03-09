using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.English;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Enrich
{
    public record EnrichEnglishSequenceCommand(Guid SequenceId) : IEventDrivenCommand;

    public class EnrichEnglishSequenceCommandHandler : CommandHandlerBase<EnrichEnglishSequenceCommand>
    {
        private readonly IExplanationRepository explanationRepository;
        private readonly ISequenceRepository sequenceRepository;
        private readonly IEnglishTranslatorGateway translatorGateway;

        public EnrichEnglishSequenceCommandHandler(
            ISequenceRepository sequenceRepository,
            IExplanationRepository explanationRepository,
            IEnglishTranslatorGateway translatorGateway)
        {
            this.sequenceRepository = sequenceRepository;
            this.explanationRepository = explanationRepository;
            this.translatorGateway = translatorGateway;
        }

        protected override async Task<IReadOnlyCollection<IDomainEvent>> Handle(EnrichEnglishSequenceCommand command)
        {
            Sequence? sequence = await this.sequenceRepository.GetOne(command.SequenceId);
            if (sequence is null)
            {
                return ArraySegment<IDomainEvent>.Empty;
            }

            Explanation? existingExplanation = this.explanationRepository.TryGetByTarget(sequence.Word.Value);

            Explanation explanation = existingExplanation ?? this.translatorGateway.GetExplanation(sequence.Word.Value);

            List<IDomainEvent> events = new()
            {
                new ExplanationAssignedToSequenceEvent(sequence.SequenceId, explanation.ExplanationId)
            };
            if (existingExplanation is null)
            {
                events.Add(new ExplanationAddedEvent(explanation));
            }

            return events;
        }
    }
}