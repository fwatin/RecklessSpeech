using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Enrich;
using RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Gateways;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.Enrich
{
    public class CaseOfAlreadyExistingExplanation
    {
        private readonly ExplanationBuilder explanationBuilder;
        private readonly InMemoryTestExplanationRepository explanationRepository;
        private readonly SequenceBuilder sequenceBuilder;
        private readonly InMemoryTestSequenceRepository sequenceRepository;
        private readonly EnrichDutchSequenceCommandHandler sut;

        public CaseOfAlreadyExistingExplanation()
        {
            this.sequenceRepository = new();
            this.explanationRepository = new();

            this.sut = new(
                this.sequenceRepository,
                this.explanationRepository,
                new StubDictionaryGateway());

            this.explanationBuilder = ExplanationBuilder.Create(Guid.Parse("AE97DE7B-CDDC-47DA-822F-D832C85D150B")) with
            {
                Target = new("house")
            };

            this.sequenceBuilder = SequenceBuilder.Create(Guid.Parse("43F23F72-6302-4E8B-BF2A-72A5474AA3C3")) with
            {
                Word = new("house")
            };
        }

        [Fact]
        public async Task Should_Not_Add_Explanation_If_Already_exists()
        {
            //Arrange
            this.explanationRepository.Feed(this.explanationBuilder);
            this.sequenceRepository.Feed(this.sequenceBuilder);
            EnrichDutchSequenceCommand command = this.sequenceBuilder.BuildEnrichCommand();

            //Act
            IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(command, CancellationToken.None);

            //Assert
            events.Count.Should().Be(1);
            events.First().Should().BeOfType<ExplanationAssignedToSequenceEvent>();
        }
    }
}