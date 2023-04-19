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
        private readonly IChatGptGateway chatGptGateway;
        private readonly ISequenceRepository sequenceRepository;

        public EnrichDutchSequenceCommandHandler(
            ISequenceRepository sequenceRepository,
            IDutchTranslatorGateway dutchTranslatorGateway,
            IChatGptGateway chatGptGateway)
        {
            this.sequenceRepository = sequenceRepository;
            this.dutchTranslatorGateway = dutchTranslatorGateway;
            this.chatGptGateway = chatGptGateway;
        }

        public Task<Unit> Handle(EnrichDutchSequenceCommand command, CancellationToken cancellationToken)
        {
            Sequence? sequence = this.sequenceRepository.GetOne(command.SequenceId);
            if (sequence is null)
            {
                return Task.FromResult(Unit.Value);
            }

            Explanation translationWithDictionary = this.dutchTranslatorGateway.GetExplanation(sequence.Word.Value);
            Explanation explanationWithChatGpt = this.chatGptGateway.GetExplanation(sequence.Word.Value);
            
            sequence.Explanations.Add(translationWithDictionary);
            sequence.Explanations.Add(explanationWithChatGpt);

            return Task.FromResult(Unit.Value);
        }

    }
}