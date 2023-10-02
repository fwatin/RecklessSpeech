using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Sequences.Exceptions;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Sequences
{
    public class ImportSequenceCommandHandler : IRequestHandler<ImportSequenceCommand>
    {
        private readonly ISequenceRepository sequenceRepository;
        private readonly IMediaRepository mediaRepository;

        public ImportSequenceCommandHandler(ISequenceRepository sequenceRepository, IMediaRepository mediaRepository)
        {
            this.sequenceRepository = sequenceRepository;
            this.mediaRepository = mediaRepository;
        }

        public async Task<Unit> Handle(ImportSequenceCommand request, CancellationToken cancellationToken)
        {
            MediaId mediaId = MediaId.Create(request.MediaId);

            await this.ImportMedia(request.LeftImageBase64, request.RightImageBase64, mediaId,
                request.Mp3Base64);

            if (request.Word is null) throw new UndefinedWordException();
            
            Word word = Word.Create(request.Word);

            SentenceTranslations sentenceTranslations = SentenceTranslations.Create(
                request.HumanTranslation,
                request.MachineTranslation);

            OriginalSentences originalSentences = OriginalSentences.Create(request.OriginalSentences.ToList());

            AudioFileNameWithExtension audio =
                AudioFileNameWithExtension.Create($"{request.MediaId.ToString()}.mp3");


            TranslatedWord translatedWord =
                TranslatedWord.Create(string.Join(", ", request.WordTranslations));

            HtmlContent htmlContent = HtmlContent.Create(mediaId, originalSentences, word, request.Title);

            Sequence sequence = Sequence.Create(Guid.NewGuid(),
                htmlContent,
                audio,
                word,
                translatedWord,
                originalSentences,
                sentenceTranslations,
                mediaId, new());

            this.sequenceRepository.Add(sequence);

            return Unit.Value;
        }

        private async Task ImportMedia(string? dtoLeftImage, string? dtoRightImage, MediaId mediaId,
            string? mp3InBase64)
        {
            if (dtoLeftImage is not null)
            {
                byte[] prev = Convert.FromBase64String(dtoLeftImage);
                await this.SaveInMediaRepository($"{mediaId.Value}_prev.jpg", prev);
            }

            if (dtoRightImage is not null)
            {
                byte[] next = Convert.FromBase64String(dtoRightImage);
                await this.SaveInMediaRepository($"{mediaId.Value}_next.jpg", next);
            }

            if (mp3InBase64 is not null)
            {
                byte[] mp3 = Convert.FromBase64String(mp3InBase64);
                await this.SaveInMediaRepository($"{mediaId.Value}.mp3", mp3);
            }
        }

        private async Task SaveInMediaRepository(string entryFullName, byte[] content)
        {
            string[] allowedExtensions = { ".mp3", ".jpg" };
            string extension = Path.GetExtension(entryFullName);
            if (allowedExtensions.Contains(extension))
            {
                string fileName = Path.GetFileName(entryFullName);
                await this.mediaRepository.SaveInMediaCollection(fileName, content);
            }
        }
    }
}