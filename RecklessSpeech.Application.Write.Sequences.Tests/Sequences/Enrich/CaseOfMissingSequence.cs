using RecklessSpeech.Application.Write.Sequences.Commands.Sequences.Enrich;
using RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Gateways;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.Enrich
{
    public class CaseOfMissingSequence
    {
        private readonly InMemoryTestSequenceRepository sequenceRepository;
        private readonly EnrichDutchSequenceCommandHandler sut;

        public CaseOfMissingSequence()
        {
            this.sequenceRepository = new();

            this.sut = new(this.sequenceRepository,
                new DummyExplanationRepository(),
                new DummyDictionaryGateway());
        }

        [Fact]
        public async Task Should_do_nothing_if_sequence_not_found()
        {
            //Arrange
            SequenceBuilder sequenceBuilder =
                SequenceBuilder.Create(Guid.Parse("2D40A46C-A850-4A44-8DC5-1300B1318A8C"));
            this.sequenceRepository.Feed(sequenceBuilder.BuildDomain());

            IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(SequenceBuilder
                .Create(Guid.Parse("C5686F57-60F4-483D-B21F-C2CB726EDC23"))
                .BuildEnrichCommand(), CancellationToken.None);

            events.Should().BeEmpty();
        }
    }
}