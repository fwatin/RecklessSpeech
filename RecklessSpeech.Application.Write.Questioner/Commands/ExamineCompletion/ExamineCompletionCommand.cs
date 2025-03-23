

using MediatR;
using RecklessSpeech.Domain.Questioner;

namespace RecklessSpeech.Application.Write.Questioner.Commands.ExamineCompletion
{
    public record InterestResult(IReadOnlyList<string> Interests);

    public class ExamineCompletionCommand(Completion completion) : IRequest<InterestResult>;
}