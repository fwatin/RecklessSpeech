namespace RecklessSpeech.Infrastructure.Read;

public sealed class SequenceNotFoundReadException : Exception
{
    public SequenceNotFoundReadException(Guid sequenceId) 
        : base($"Sequence with id = {sequenceId} was not found")
    {
        this.Data["sequenceId"] = sequenceId;
    }
}