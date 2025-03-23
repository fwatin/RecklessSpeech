using MediatR;

namespace RecklessSpeech.Application.Write.Questioner.Commands.ExamineCompletion
{
    public class ExamineCompletionCommandHandler : IRequestHandler<ExamineCompletionCommand, InterestResult>
    {
        public ExamineCompletionCommandHandler()
        {
            
        }
        
        public Task<InterestResult> Handle(ExamineCompletionCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}