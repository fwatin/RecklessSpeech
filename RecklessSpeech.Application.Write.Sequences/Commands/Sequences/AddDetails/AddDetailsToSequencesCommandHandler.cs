using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Domain.Shared;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.AddDetails
{
    public class AddDetailsToSequencesCommandHandler : CommandHandlerBase<AddDetailsToSequencesCommand>
    {
        private readonly ISequenceRepository sequenceRepository;

        public AddDetailsToSequencesCommandHandler(ISequenceRepository sequenceRepository) =>
            this.sequenceRepository = sequenceRepository;

        protected override async Task<IReadOnlyCollection<IDomainEvent>> Handle(AddDetailsToSequencesCommand command)
        {
            List<IDomainEvent> events = new();

            //parcourir les details
            foreach (Class1 item in command.Dtos)
            {
                Sequence? sequence = await this.sequenceRepository.GetOneByWord(item.word.text);
                if (sequence is null)
                {
                    continue;
                }

                events.Add(new SetTranslatedWordEvent(
                    sequence.SequenceId,
                    TranslatedWord.Create(item.wordTranslationsArr.First())));
            }

            return events;
        }
    }
}