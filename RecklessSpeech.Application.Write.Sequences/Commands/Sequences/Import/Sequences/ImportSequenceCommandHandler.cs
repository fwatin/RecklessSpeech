using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Explanations;
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
            Word word = Word.Create(request.Dto.Word);
            SentenceTranslations sentenceTranslations = SentenceTranslations.Create(request.Dto.TranslatedSentence.HumanTranslation,
                request.Dto.TranslatedSentence.MachineTranslation);
            OriginalSentences originalSentences = OriginalSentences.Create(request.Dto.OriginalSentences.ToList());
            AudioFileNameWithExtension audio = AudioFileNameWithExtension.Create($"{request.Dto.MediaId.ToString()}.mp3");
            MediaId mediaId = MediaId.Create(request.Dto.MediaId);
            TranslatedWord translatedWord = TranslatedWord.Create(string.Join(", ", request.Dto.TranslatedWordPropositions));

            HtmlContent htmlContent = HtmlContent.Create(mediaId, originalSentences, word, request.Dto.Title);

            Sequence sequence = Sequence.Create(Guid.NewGuid(),
                htmlContent,
                audio,
                word,
                translatedWord,
                originalSentences,
                sentenceTranslations,
                mediaId, new());

            this.sequenceRepository.Add(sequence);

            return Task.FromResult(Unit.Value);
        }
    }
}