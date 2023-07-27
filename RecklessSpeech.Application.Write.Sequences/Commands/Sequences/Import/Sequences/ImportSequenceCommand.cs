using ExCSS;
using HtmlAgilityPack;
using RecklessSpeech.Application.Write.Sequences.Ports;
using RecklessSpeech.Domain.Sequences.Sequences;
using System.Text;

namespace RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Import.Sequences
{
    public record ImportSequenceCommand(string Word, string TranslatedSentence, string AudioFilenameWithExtension, long MediaId) : IRequest;

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
            AudioFileNameWithExtension audio = AudioFileNameWithExtension.Create(request.AudioFilenameWithExtension);
            MediaId mediaId = MediaId.Create(request.MediaId);

            HtmlContent htmlContent = GetHtmlContent(mediaId, translatedSentence);

            Sequence sequence = Sequence.Create(Guid.NewGuid(),
                htmlContent,
                audio,
                word,
                translatedSentence,
                mediaId);

            this.sequenceRepository.Add(sequence);
            
            return Task.FromResult(Unit.Value);
        }
    }
}