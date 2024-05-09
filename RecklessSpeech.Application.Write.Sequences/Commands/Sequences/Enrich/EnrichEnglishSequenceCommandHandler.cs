using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.English;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Enrich
{
    public record EnrichEnglishSequenceCommand(Guid SequenceId) : IRequest;

    public class EnrichEnglishSequenceCommandHandler : IRequestHandler<EnrichEnglishSequenceCommand>
    {
        private readonly ISequenceRepository sequenceRepository;
        private readonly ITranslatorGateway translatorGateway;
        private readonly IChatGptGateway chatGptGateway;

        public EnrichEnglishSequenceCommandHandler(
            ISequenceRepository sequenceRepository,
            ITranslatorGateway translatorGateway,
            IChatGptGateway chatGptGateway)
        {
            this.sequenceRepository = sequenceRepository;
            this.translatorGateway = translatorGateway;
            this.chatGptGateway = chatGptGateway;
        }

        public async Task<Unit> Handle(EnrichEnglishSequenceCommand command, CancellationToken cancellationToken)
        {
            Sequence? sequence = this.sequenceRepository.GetOne(command.SequenceId);
            if (sequence is null)
            {
                return Unit.Value;
            }
            
            if (sequence.OriginalSentences is not null)
            {
                Explanation explanationWithChatGpt =
                    await this.chatGptGateway.GetExplanation(sequence);
                sequence.Explanations.Add(explanationWithChatGpt);
            }

            Explanation explanation = this.translatorGateway.GetExplanation(sequence.ContentToGuessInNativeLanguage());
            sequence.Explanations.Add(explanation);
            

            return Unit.Value;
        }
    }
}