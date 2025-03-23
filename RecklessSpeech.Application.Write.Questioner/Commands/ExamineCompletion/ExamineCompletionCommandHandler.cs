using MediatR;
using RecklessSpeech.Application.Write.Questioner.Ports;

namespace RecklessSpeech.Application.Write.Questioner.Commands.ExamineCompletion
{
    public class ExamineCompletionCommandHandler : IRequestHandler<ExamineCompletionCommand, InterestResult>
    {
        private readonly IReadNoteGateway readNoteGateway;

        public ExamineCompletionCommandHandler(IReadNoteGateway readNoteGateway)
        {
            this.readNoteGateway = readNoteGateway;
        }

        public async Task<InterestResult> Handle(ExamineCompletionCommand command, CancellationToken cancellationToken)
        {
            var relatedNotes = await this.readNoteGateway.GetBySubject(command.Subject);
            
            
            return new(new List<string>());
        }
    }
}