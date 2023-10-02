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
            await this.ImportMedia(request.Dto.LeftImage, request.Dto.RightImage, request.Dto.MediaId.ToString(),
                request.Dto.Mp3);

            Word word = Word.Create(request.Dto.Word);

            SentenceTranslations sentenceTranslations = SentenceTranslations.Create(
                request.Dto.HumanTranslation,
                request.Dto.MachineTranslation);

            OriginalSentences originalSentences = OriginalSentences.Create(request.Dto.OriginalSentences.ToList());

            AudioFileNameWithExtension audio =
                AudioFileNameWithExtension.Create($"{request.Dto.MediaId.ToString()}.mp3");

            MediaId mediaId = MediaId.Create(request.Dto.MediaId);

            TranslatedWord translatedWord =
                TranslatedWord.Create(string.Join(", ", request.Dto.TranslatedWordPropositions));

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

            return Unit.Value;
        }

        private async Task ImportMedia(string? dtoLeftImage, string? dtoRightImage, string timeModified,
            string? mp3InBase64)
        {
            string? prevBase64 = dtoLeftImage;
            if (prevBase64 is not null)
            {
                byte[] prev = Convert.FromBase64String(prevBase64);
                await this.SaveMedia($"{timeModified}_prev.jpg", prev);
            }

            string? nextBase64 = dtoRightImage;
            if (nextBase64 is not null)
            {
                byte[] next = Convert.FromBase64String(nextBase64);
                await this.SaveMedia($"{timeModified}_next.jpg", next);
            }

            //mp3
            if (mp3InBase64 is not null)
            {
                byte[] mp3 = Convert.FromBase64String(mp3InBase64);
                await this.SaveMedia($"{timeModified}.mp3", mp3);
            }
        }

        private async Task SaveMedia(string entryFullName, byte[] content)
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