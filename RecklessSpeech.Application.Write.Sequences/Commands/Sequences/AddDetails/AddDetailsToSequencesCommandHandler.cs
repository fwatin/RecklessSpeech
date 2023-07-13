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
            foreach (Class1 item in command.Dtos)
            {
                IEnumerable<Sequence> sequences = this.sequenceRepository.GetOneByMediaId(item.timeModified_ms);

                foreach (Sequence sequence in sequences)
                {
                    sequence.TranslatedWord = TranslatedWord.Create(item.wordTranslationsArr.First());
                    StringBuilder sentences = new();

                    if (item.context?.phrase?.subtitles is null) return Task.FromResult(Unit.Value);

                    foreach (var sentence in item.context.phrase.subtitles.Values)
                    {
                        sentences.Append(sentence);
                    }

                    sequence.OriginalSentence = OriginalSentence.Create(sentences.ToString());
                }
            }

            return Task.FromResult(Unit.Value);
        }
    }
}