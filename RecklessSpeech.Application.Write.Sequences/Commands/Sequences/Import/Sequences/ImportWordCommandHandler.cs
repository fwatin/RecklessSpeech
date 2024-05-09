using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Sequences.Exceptions;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Sequences
{
    public class ImportWordCommandHandler : IRequestHandler<ImportWordCommand>
    {
        private readonly ISequenceRepository sequenceRepository;
        private readonly IMediaRepository mediaRepository;

        public ImportWordCommandHandler(ISequenceRepository sequenceRepository, IMediaRepository mediaRepository)
        {
            this.sequenceRepository = sequenceRepository;
            this.mediaRepository = mediaRepository;
        }

        public async Task<Unit> Handle(ImportWordCommand request, CancellationToken cancellationToken)
        {
            Media media = Media.Create(
                request.MediaId,
                request.LeftImageBase64,
                request.RightImageBase64,
                request.Mp3Base64);

            await this.ImportMedia(media);

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

            HtmlContent htmlContent = HtmlContent.Create(media, originalSentences, word, request.Title);

            Sequence sequence = Sequence.Create(Guid.NewGuid(),
                htmlContent,
                audio,
                word,
                translatedWord,
                originalSentences,
                sentenceTranslations,
                media, new());

            this.sequenceRepository.Add(sequence);

            return Unit.Value;
        }

        private async Task ImportMedia(Media media)
        {
            if (media.LeftImage is not null)
            {
                byte[] prev = Convert.FromBase64String(media.LeftImage);
                await this.SaveInMediaRepository($"{media.MediaId}_prev.jpg", prev);
            }

            if (media.RightImage is not null)
            {
                byte[] next = Convert.FromBase64String(media.RightImage);
                await this.SaveInMediaRepository($"{media.MediaId}_next.jpg", next);
            }

            if (media.Mp3 is not null)
            {
                byte[] mp3 = Convert.FromBase64String(media.Mp3);
                await this.SaveInMediaRepository($"{media.MediaId}.mp3", mp3);
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