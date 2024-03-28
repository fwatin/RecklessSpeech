using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Domain.Sequences.Notes
{
    public record NoteDto(Question Question, Answer Answer, After After, Source Source, Audio Audio, List<Tag> Tags);
}