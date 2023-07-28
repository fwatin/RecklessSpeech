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
            TranslatedSentence translatedSentence = TranslatedSentence.Create(request.TranslatedSentence);
            OriginalSentence originalSentence = OriginalSentence.Create(request.OriginalSentence);
            AudioFileNameWithExtension audio = AudioFileNameWithExtension.Create($"{request.MediaId.ToString()}.mp3");
            MediaId mediaId = MediaId.Create(request.MediaId);

            HtmlContent htmlContent = HtmlContent.Create(mediaId, originalSentence, word, request.Title);

            Sequence sequence = Sequence.Create(Guid.NewGuid(),
                htmlContent,
                audio,
                word,
                originalSentence,
                translatedSentence,
                mediaId);

            this.sequenceRepository.Add(sequence);
            
            return Task.FromResult(Unit.Value);
        }
    }
}