namespace RecklessSpeech.Application.Read.Queries.Sequences.GetOne;

public sealed class SequenceNotFoundReadException : Exception
{
    public SequenceNotFoundReadException(Guid sequenceId) 
        : base($"Sequence with id = {sequenceId} was not found")
    {
        this.Data["sequenceId"] = sequenceId;
    }
}