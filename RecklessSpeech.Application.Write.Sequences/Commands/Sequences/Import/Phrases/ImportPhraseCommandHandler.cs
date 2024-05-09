using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Sequences.Exceptions;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Sequences.Sequences;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Phrases
{
    public class ImportPhraseCommandHandler : IRequestHandler<ImportPhraseCommand>
    {
        private readonly ISequenceRepository sequenceRepository;
        private readonly IMediaRepository mediaRepository;

        public ImportPhraseCommandHandler(ISequenceRepository sequenceRepository, IMediaRepository mediaRepository)
        {
            this.sequenceRepository = sequenceRepository;
            this.mediaRepository = mediaRepository;
        }

        public async Task<Unit> Handle(ImportPhraseCommand request, CancellationToken cancellationToken)
        {
            Media media = Media.Create(
                request.MediaId,
                request.LeftImageBase64,
                request.RightImageBase64,
                request.Mp3Base64);

            await this.ImportMedia(media);

            if (request.Phrase is null) throw new UndefinedWordException();

            Phrase phrase = Phrase.Create(request.Phrase);

            SentenceTranslations sentenceTranslations = SentenceTranslations.Create(
                request.HumanTranslation,
                request.MachineTranslation);

            OriginalSentences originalSentences = OriginalSentences.Create(request.OriginalSentences.ToList());

            AudioFileNameWithExtension audio = AudioFileNameWithExtension.Create($"{request.MediaId.ToString()}.mp3");

            HtmlContent htmlContent = HtmlContent.Create(media, originalSentences, phrase, request.Title);

            PhraseSequence sequence = PhraseSequence.Create(Guid.NewGuid(),
                htmlContent,
                audio,
                originalSentences,
                sentenceTranslations,
                media, 
                new(),
                Language.GetLanguageFromCode(request.LanguageCode));

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