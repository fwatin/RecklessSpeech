using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Application.Write.Sequences.Ports.TranslatorGateways.Dutch;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Enrich
{
    public record EnrichSequenceCommand(Guid SequenceId) : IRequest;

    public class EnrichSequenceCommandHandler : IRequestHandler<EnrichSequenceCommand>
    {
        private readonly ITranslatorGatewayFactory translatorGatewayFactory;
        private readonly IChatGptGateway chatGptGateway;
        private readonly ISequenceRepository sequenceRepository;

        public EnrichSequenceCommandHandler(
            ISequenceRepository sequenceRepository,
            ITranslatorGatewayFactory translatorGatewayFactory,
            IChatGptGateway chatGptGateway)
        {
            this.sequenceRepository = sequenceRepository;
            this.translatorGatewayFactory = translatorGatewayFactory;
            this.chatGptGateway = chatGptGateway;
        }

        public async Task<Unit> Handle(EnrichSequenceCommand command, CancellationToken cancellationToken)
        {
            Sequence? sequence = this.sequenceRepository.GetOne(command.SequenceId);
            if (sequence is null)
            {
                return Unit.Value;
            }

            string? singleWordTranslation = null;

            if (sequence.OriginalSentences is not null)
            {
                Explanation? explanationWithChatGpt = await this.chatGptGateway.GetExplanation(sequence);

                if (explanationWithChatGpt?.RawExplanation is not null && sequence is WordSequence word)
                {
                    singleWordTranslation =
                        await this.chatGptGateway.GetSingleWordTranslation(word, explanationWithChatGpt);
                    word.Translation = singleWordTranslation;
                }

                if (explanationWithChatGpt is not null)
                {
                    sequence.Explanations.Add(explanationWithChatGpt);
                }
            }

            if (sequence is WordSequence)
            {
                ITranslatorGateway translatorGateway =
                    this.translatorGatewayFactory.GetTranslatorGateway(sequence.Language);
                Explanation translationWithDictionary =
                    translatorGateway.GetExplanation(sequence.ContentToGuessInNativeLanguage());

                sequence.Explanations.Add(translationWithDictionary);
            }

            if (singleWordTranslation is not null)
            {
                foreach (Explanation explanation in sequence.Explanations)
                {
                    explanation.HighlightTerm(singleWordTranslation);
                }
            }

            return Unit.Value;
        }
    }
}