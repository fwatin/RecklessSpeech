using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.English;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Enrich
{
    public record EnrichEnglishSequenceCommand(Guid SequenceId) : IRequest;

    public class EnrichEnglishSequenceCommandHandler : IRequestHandler<EnrichEnglishSequenceCommand>
    {
        private readonly ISequenceRepository sequenceRepository;
        private readonly IEnglishTranslatorGateway translatorGateway;

        public EnrichEnglishSequenceCommandHandler(
            ISequenceRepository sequenceRepository,
            IEnglishTranslatorGateway translatorGateway)
        {
            this.sequenceRepository = sequenceRepository;
            this.translatorGateway = translatorGateway;
        }

        public Task<Unit> Handle(EnrichEnglishSequenceCommand command, CancellationToken cancellationToken)
        {
            Sequence? sequence = this.sequenceRepository.GetOne(command.SequenceId);
            if (sequence is null)
            {
                return Task.FromResult(Unit.Value);
            }

            Explanation explanation = this.translatorGateway.GetExplanation(sequence.Word.Value);
            sequence.Explanations.Add(explanation);
            

            return Task.FromResult(Unit.Value);
        }
    }
}