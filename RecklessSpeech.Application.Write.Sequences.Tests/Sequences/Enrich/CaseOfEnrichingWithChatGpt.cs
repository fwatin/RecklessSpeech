﻿namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.Enrich
{
    public class CaseOfEnrichingWithChatGpt
    {
        private readonly EnrichSequenceCommandHandler sut;
        private readonly InMemorySequenceRepository sequenceRepository;
        private readonly StubChatGptGateway chatGptGateway;

        public CaseOfEnrichingWithChatGpt()
        {
            this.sequenceRepository = new();
            this.chatGptGateway = new();
            this.sut = new(this.sequenceRepository, new EmptyTranslatorGatewayFactory(), this.chatGptGateway);
        }

        [Fact]
        public async Task Should_add_in_sequence_content()
        {
            //Arrange
            SequenceBuilder sequenceBuilder = SequenceBuilder.Create() with
            {
                OriginalSentence = new(new(){"tiens j'ai laché une caisse"})
            };
            this.sequenceRepository.Add(sequenceBuilder);
            ExplanationBuilder explanation = ExplanationBuilder.Create() with
            {
                Content = new("C'est une expression pour dire prout")
            };
            this.chatGptGateway.Feed(explanation);

            EnrichSequenceCommand command = new(sequenceBuilder.SequenceId.Value);

            //Act
            await this.sut.Handle(command, CancellationToken.None);

            //Assert
            this.sequenceRepository.GetAll().Single().Explanations
                .Any(x => x.ExplanationInHtml.Value.Contains("C'est une expression pour dire prout")).Should().BeTrue();
        }
    }
}