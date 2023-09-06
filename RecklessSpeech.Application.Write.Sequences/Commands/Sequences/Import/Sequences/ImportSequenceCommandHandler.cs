using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Sequences
{
    public class ImportSequenceCommandHandler : IRequestHandler<ImportSequenceCommand>
    {
        private readonly ISequenceRepository sequenceRepository;
        public ImportSequenceCommandHandler(ISequenceRepository sequenceRepository)
        {
            this.sequenceRepository = sequenceRepository;
        }
        public Task<Unit> Handle(ImportSequenceCommand request, CancellationToken cancellationToken)
        {
            Word word = Word.Create(request.Word);
            SentenceTranslations sentenceTranslations = SentenceTranslations.Create(request.TranslatedSentence.HumanTranslation,
                request.TranslatedSentence.MachineTranslation);
            OriginalSentences originalSentences = OriginalSentences.Create(request.OriginalSentences.ToList());
            AudioFileNameWithExtension audio = AudioFileNameWithExtension.Create($"{request.MediaId.ToString()}.mp3");
            MediaId mediaId = MediaId.Create(request.MediaId);
            TranslatedWord translatedWord = TranslatedWord.Create(string.Join(", ", request.TranslatedWordPropositions));

            HtmlContent htmlContent = HtmlContent.Create(mediaId, originalSentences, word, request.Title);

            Sequence sequence = Sequence.Create(Guid.NewGuid(),
                htmlContent,
                audio,
                word,
                translatedWord,
                originalSentences,
                sentenceTranslations,
                mediaId);

            this.sequenceRepository.Add(sequence);

            return Task.FromResult(Unit.Value);
        }
    }
}