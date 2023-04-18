using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Enrich;
using RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Gateways;
using RecklessSpeech.Infrastructure.Sequences.Repositories;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.Enrich
{
    public class CaseOfEnrichSuccessful
    {
        private readonly SequenceBuilder sequenceBuilder;
        private readonly EnrichDutchSequenceCommandHandler sut;
        private readonly InMemorySequenceRepository sequenceRepository;

        public CaseOfEnrichSuccessful()
        {
            StubDutchTranslatorGateway stubDutchTranslatorGateway = new();
            ExplanationBuilder explanationBuilder =
                ExplanationBuilder.Create(Guid.Parse("F189810B-B15E-4360-911C-5FBCCA771887")) with
            {
                Content = new("du pain")
            };
            stubDutchTranslatorGateway.Feed(explanationBuilder);

            this.sequenceRepository = new();
            this.sut = new(
                this.sequenceRepository,
                stubDutchTranslatorGateway);
            

            this.sequenceBuilder = SequenceBuilder.Create(Guid.Parse("5CFF7781-7892-4172-9656-8EF0E6A76D2C"))with
            {
                Word = new("brood"), Explanation = explanationBuilder
            };
        }

        [Fact]
        public async Task Should_enrich_a_sequence_with_explanation()
        {
            //Arrange
            this.sequenceRepository.Add(this.sequenceBuilder);
            EnrichDutchSequenceCommand command = this.sequenceBuilder.BuildEnrichCommand();

            //Act
            await this.sut.Handle(command, CancellationToken.None);

            //Assert
            this.sequenceRepository.All.Single().Explanation!.Content.Value.Should().Contain("pain");
        }
    }
}