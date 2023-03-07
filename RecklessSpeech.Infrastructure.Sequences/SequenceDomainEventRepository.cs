using RecklessSpeech.Domain.Sequences.Explanations;
using RecklessSpeech.Domain.Sequences.Sequences;
using RecklessSpeech.Domain.Shared;
using RecklessSpeech.Infrastructure.Entities;
using RecklessSpeech.Infrastructure.Orchestration.Dispatch;

namespace RecklessSpeech.Infrastructure.Sequences
{
    public class SequenceDomainEventRepository : IDomainEventRepository
    {
        private readonly ISequencesDbContext dbContext;

        public SequenceDomainEventRepository(ISequencesDbContext dbContext) => this.dbContext = dbContext;

        public async Task ApplyEvent(IDomainEvent @event)
        {
            switch (@event)
            {
                case ImportedSequenceEvent requestedEvent:
                    await this.Handle(requestedEvent);
                    break;

                case ExplanationAssignedToSequenceEvent enrichSequenceEvent:
                    await this.Handle(enrichSequenceEvent);
                    break;

                case ExplanationAddedEvent addExplanationEvent:
                    await this.Handle(addExplanationEvent);
                    break;

                case SetTranslatedWordEvent translatedWordEvent:
                    await this.Handle(translatedWordEvent);
                    break;


                case AssignLanguageDictionaryInASequenceEvent setLanguageDictionaryInASequenceEvent:
                    await this.Handle(setLanguageDictionaryInASequenceEvent);
                    break;

                default: throw new("event type is not known for ApplyEvent");
            }
        }

        private async Task Handle(ImportedSequenceEvent @event)
        {
            SequenceEntity entity = new()
            {
                Id = @event.Id.Value,
                HtmlContent = @event.HtmlContent.Value,
                AudioFileNameWithExtension = @event.AudioFileNameWithExtension.Value,
                Tags = @event.Tags.Value,
                Word = @event.Word.Value,
                TranslatedSentence = @event.TranslatedSentence.Value,
                TranslatedWord = @event.TranslatedWord?.Value
            };

            this.dbContext.Sequences.Add(entity);

            await this.SaveChangesAsync();
        }

        private async Task Handle(ExplanationAddedEvent addedEvent)
        {
            ExplanationEntity entity = new()
            {
                Id = addedEvent.Explanation.ExplanationId.Value,
                Content = addedEvent.Explanation.Content.Value,
                Target = addedEvent.Explanation.Target.Value,
                SourceUrl = addedEvent.Explanation.SourceUrl.Value
            };

            this.dbContext.Explanations.Add(entity); //passer en addAsync plus tard quand EF

            await this.SaveChangesAsync();
        }

        private async Task Handle(SetTranslatedWordEvent setTranslatedWordEvent)
        {
            SequenceEntity sequenceEntity =
                this.dbContext.Sequences.Single(x => x.Id == setTranslatedWordEvent.SequenceId.Value);

            sequenceEntity.TranslatedWord = setTranslatedWordEvent.TranslatedWord.Value;

            await this.SaveChangesAsync();
        }

        private async Task Handle(ExplanationAssignedToSequenceEvent @event)
        {
            SequenceEntity sequenceEntity = this.dbContext.Sequences.Single(x => x.Id == @event.SequenceId.Value);

            sequenceEntity.ExplanationId = @event.ExplanationId.Value;

            await this.SaveChangesAsync();
        }

        private async Task Handle(AssignLanguageDictionaryInASequenceEvent @event)
        {
            SequenceEntity sequenceEntity = this.dbContext.Sequences.Single(x => x.Id == @event.SequenceId.Value);

            sequenceEntity.LanguageDictionaryId = @event.LanguageDictionaryId.Value;

            await this.SaveChangesAsync();
        }

        private async Task SaveChangesAsync() => await Task.CompletedTask;
    }
}