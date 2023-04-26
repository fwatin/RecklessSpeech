namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.Enrich
{
    public class CaseOfMissingSequence
    {
        private readonly InMemorySequenceRepository sequenceRepository;
        private readonly EnrichDutchSequenceCommandHandler sut;

        public CaseOfMissingSequence()
        {
            this.sequenceRepository = new();

            this.sut = new(this.sequenceRepository,
                new DummyDictionaryGateway(),
                new DummyChatGptGateway());
        }

        [Fact]
        public async Task Should_do_nothing_if_sequence_not_found()
        {
            //Arrange
            SequenceBuilder sequenceBuilder = SequenceBuilder.Create(Guid.Parse("2D40A46C-A850-4A44-8DC5-1300B1318A8C"));
            this.sequenceRepository.Add(sequenceBuilder.BuildDomain());

            await this.sut.Handle(SequenceBuilder.Create(Guid.NewGuid()).BuildEnrichCommand(), CancellationToken.None);

            this.sequenceRepository.All.Single().Explanations.Should().BeEmpty();
        }
    }
}