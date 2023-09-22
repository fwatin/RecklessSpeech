using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Italian;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Enrich
{
    public record EnrichItalianSequenceCommand(Guid SequenceId) : IRequest;

    public class EnrichItalianSequenceCommandHandler : IRequestHandler<EnrichItalianSequenceCommand>
    {
        private readonly ISequenceRepository sequenceRepository;
        private readonly IItalianTranslatorGateway translatorGateway;
        private readonly IChatGptGateway chatGptGateway;

        public EnrichItalianSequenceCommandHandler(
            ISequenceRepository sequenceRepository,
            IItalianTranslatorGateway translatorGateway,
            IChatGptGateway chatGptGateway)
        {
            this.sequenceRepository = sequenceRepository;
            this.translatorGateway = translatorGateway;
            this.chatGptGateway = chatGptGateway;
        }

        public async Task<Unit> Handle(EnrichItalianSequenceCommand command, CancellationToken cancellationToken)
        {
            Sequence? sequence = this.sequenceRepository.GetOne(command.SequenceId);
            if (sequence is null)
            {
                return Unit.Value;
            }
            
            if (sequence.OriginalSentences is not null)
            {
                Explanation explanationWithChatGpt =
                    await this.chatGptGateway.GetExplanation(sequence.Word.Value, sequence.OriginalSentences,new Italian());
                sequence.Explanations.Add(explanationWithChatGpt);
            }

            Explanation explanation = this.translatorGateway.GetExplanation(sequence.Word.Value);
            sequence.Explanations.Add(explanation);
            

            return Unit.Value;
        }
    }
}