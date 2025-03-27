using MediatR;
using RecklessSpeech.Application.Write.Questioner.Ports;

namespace RecklessSpeech.Application.Write.Questioner.Commands.ExamineCompletion
{
    public class ExamineCompletionCommandHandler : IRequestHandler<ExamineCompletionCommand, InterestResult>
    {
        private readonly IReadNoteGateway readNoteGateway;
        private readonly IChatGptGateway chatGptGateway;

        public ExamineCompletionCommandHandler(IReadNoteGateway readNoteGateway, IChatGptGateway chatGptGateway)
        {
            this.readNoteGateway = readNoteGateway;
            this.chatGptGateway = chatGptGateway;
        }

        public async Task<InterestResult> Handle(ExamineCompletionCommand command, CancellationToken cancellationToken)
        {
            var relatedNotes = await this.readNoteGateway.GetBySubject(command.Subject);
            
            IReadOnlyList<string> questions = await this.chatGptGateway.GetInterests(relatedNotes, command.Completion);
            
            return new(questions);
        }
    }
}