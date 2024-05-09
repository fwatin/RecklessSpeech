using RecklessSpeech.Domain.Sequences.Explanations;

namespace RecklessSpeech.Domain.Sequences.Sequences
{
    public abstract class Sequence
    {
        public AudioFileNameWithExtension AudioFile = default!;
        public SequenceId SequenceId { get; }
        public HtmlContent HtmlContent { get; protected init; } = default!;
        public SentenceTranslations SentenceTranslations { get; protected init; } = default!;
        public List<Explanation> Explanations { get; set; }
        
        public Language Language { get; protected set; }
        public abstract string SentenceToAskChatGptExplanation { get;  }

        protected Sequence(SequenceId sequenceId)
        {
            this.SequenceId = sequenceId;
            this.Explanations = new();
        }

        public Media? Media { get; protected init; }
        public OriginalSentences? OriginalSentences { get; set; }
        public int SentToAnkiTimes { get; set; }

        public abstract string? ContentToGuessInTargetedLanguage();
        public abstract string ContentToGuessInNativeLanguage();
        public abstract string Translation { set; }
    }
}