

using MediatR;
using RecklessSpeech.Domain.Questioner;

namespace RecklessSpeech.Application.Write.Questioner.Commands.ExamineCompletion
{
    public record InterestResult(IReadOnlyList<string> Interests);

    public record ExamineCompletionCommand(Completion Completion, string Subject) : IRequest<InterestResult>;
}