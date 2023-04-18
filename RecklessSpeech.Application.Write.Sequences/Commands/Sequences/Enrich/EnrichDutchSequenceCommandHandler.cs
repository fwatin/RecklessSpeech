using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Enrich
{
    public record EnrichDutchSequenceCommand(Guid SequenceId) : IRequest;

    public class EnrichDutchSequenceCommandHandler : IRequestHandler<EnrichDutchSequenceCommand>
    {
        private readonly IDutchTranslatorGateway dutchTranslatorGateway;
        private readonly ISequenceRepository sequenceRepository;

        public EnrichDutchSequenceCommandHandler(
            ISequenceRepository sequenceRepository,
            IDutchTranslatorGateway dutchTranslatorGateway)
        {
            this.sequenceRepository = sequenceRepository;
            this.dutchTranslatorGateway = dutchTranslatorGateway;
        }

        public Task<Unit> Handle(EnrichDutchSequenceCommand command, CancellationToken cancellationToken)
        {
            Sequence? sequence = this.sequenceRepository.GetOne(command.SequenceId);
            if (sequence is null)
            {
                return Task.FromResult(Unit.Value);
            }

            Explanation existingExplanation = this.dutchTranslatorGateway.GetExplanation(sequence.Word.Value);
            sequence.Explanation = existingExplanation;

            return Task.FromResult(Unit.Value);
        }

    }
}