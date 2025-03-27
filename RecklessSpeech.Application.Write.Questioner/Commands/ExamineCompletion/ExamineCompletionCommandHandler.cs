using MediatR;
using RecklessSpeech.Application.Write.Questioner.Ports;

namespace RecklessSpeech.Application.Write.Questioner.Commands.ExamineCompletion
{
    public class ExamineCompletionCommandHandler : IRequestHandler<ExamineCompletionCommand, InterestResult>
    {
        private readonly IQuestionerReadNoteGateway questionerReadNoteGateway;
        private readonly IQuestionerChatGptGateway questionerChatGptGateway;

        public ExamineCompletionCommandHandler(IQuestionerReadNoteGateway questionerReadNoteGateway, IQuestionerChatGptGateway questionerChatGptGateway)
        {
            this.questionerReadNoteGateway = questionerReadNoteGateway;
            this.questionerChatGptGateway = questionerChatGptGateway;
        }

        public async Task<InterestResult> Handle(ExamineCompletionCommand command, CancellationToken cancellationToken)
        {
            var relatedNotes = await this.questionerReadNoteGateway.GetBySubject(command.Subject);
            
            IReadOnlyList<string> questions = await this.questionerChatGptGateway.GetInterests(relatedNotes, command.Completion);
            
            return new(questions);
        }
    }
}