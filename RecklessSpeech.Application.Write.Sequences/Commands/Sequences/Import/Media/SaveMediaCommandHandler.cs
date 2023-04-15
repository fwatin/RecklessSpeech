using RecklessSpeech.Application.Core.Commands;
using RecklessSpeech.Application.Core.Events;
using RecklessSpeech.Application.Write.Sequences.Ports;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Media
{
    public class SaveMediaCommandHandler : CommandHandlerBase<SaveMediaCommand>
    {
        private readonly IMediaRepository mediaRepository;
        public SaveMediaCommandHandler(IMediaRepository mediaRepository)
        {
            this.mediaRepository = mediaRepository;
        }

        protected override async Task<IReadOnlyCollection<IDomainEvent>> Handle(SaveMediaCommand command)
        {
            await this.mediaRepository.SaveInMediaCollection(command.EntryFullName, command.Content);
            return Array.Empty<IDomainEvent>();
        }
    }

}