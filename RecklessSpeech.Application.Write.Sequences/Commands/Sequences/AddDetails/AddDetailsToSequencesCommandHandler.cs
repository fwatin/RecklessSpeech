using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Sequences;

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
                    sequence.Translation = item.wordTranslationsArr.First();

                    if (item.context?.phrase?.subtitles is null) return Task.FromResult(Unit.Value);

                    sequence.OriginalSentences = OriginalSentences.Create(item.context.phrase.subtitles.Values.ToList());
                }
            }

            return Task.FromResult(Unit.Value);
        }
    }
}