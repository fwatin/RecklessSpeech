using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Sequences;
using System.Text;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.AddDetails
{
    public class AddDetailsToSequencesCommandHandler : IRequestHandler<AddDetailsToSequencesCommand>
    {
        private readonly ISequenceRepository sequenceRepository;

        public AddDetailsToSequencesCommandHandler(ISequenceRepository sequenceRepository) =>
            this.sequenceRepository = sequenceRepository;

        public Task<Unit> Handle(AddDetailsToSequencesCommand command, CancellationToken cancellationToken)
        {
            //parcourir les details
            foreach (Class1 item in command.Dtos)
            {
                Sequence? sequence = this.sequenceRepository.GetOneByMediaId(item.timeModified_ms);
                if (sequence is null)
                {
                    continue;
                }

                sequence.TranslatedWord = TranslatedWord.Create(item.wordTranslationsArr.First());
                StringBuilder sentences = new();
                foreach (var sentence in item.context.phrase.subtitles.Values)
                {
                    sentences.Append(sentence);
                }

                sequence.OriginalSentence = OriginalSentence.Create(sentences.ToString());
            }

            return Task.FromResult(Unit.Value);
        }
    }
}