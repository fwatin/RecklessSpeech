﻿namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.Enrich
{
    public class CaseOfEnrichSuccessful
    {
        private readonly SequenceBuilder sequenceBuilder;
        private readonly EnrichSequenceCommandHandler sut;
        private readonly InMemorySequenceRepository sequenceRepository;

        public CaseOfEnrichSuccessful()
        {
            StubTranslatorGatewayFactory stubTranslatorGatewayFactory = new();
            ExplanationBuilder explanationBuilder =
                ExplanationBuilder.Create(Guid.Parse("F189810B-B15E-4360-911C-5FBCCA771887")) with
            {
                Content = new("du pain")
            };
            stubTranslatorGatewayFactory.GetStub.Feed(explanationBuilder);

            this.sequenceRepository = new();
            this.sut = new(
                this.sequenceRepository,
                stubTranslatorGatewayFactory,
                new StubChatGptGateway());
            

            this.sequenceBuilder = SequenceBuilder.Create(Guid.Parse("5CFF7781-7892-4172-9656-8EF0E6A76D2C"))with
            {
                Word = new("brood"),
                Explanations = new() { explanationBuilder },
            };
        }

        [Fact]
        public async Task Should_enrich_a_sequence_with_explanation()
        {
            //Arrange
            this.sequenceRepository.Add(this.sequenceBuilder);
            EnrichSequenceCommand command = this.sequenceBuilder.BuildEnrichCommand();

            //Act
            await this.sut.Handle(command, CancellationToken.None);

            //Assert
            this.sequenceRepository.All.Single().Explanations[0].ExplanationInHtml.Value.Should().Contain("pain");
        }
    }
}