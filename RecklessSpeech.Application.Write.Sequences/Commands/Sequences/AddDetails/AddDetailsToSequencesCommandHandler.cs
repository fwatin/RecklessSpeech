using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Core.Events;
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
            //parcourir les details
            foreach (Class1 item in command.Dtos)
            {
                Sequence? sequence = this.sequenceRepository.GetOneByMediaId(item.timeModified_ms);
                if (sequence is null)
                {
                    continue;
                }

                sequence.TranslatedWord = TranslatedWord.Create(item.wordTranslationsArr.First()); 
            }

            return Task.FromResult(Unit.Value);
        }
    }
}