using RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles;
using RecklessSpeech.Application.Write.Sequences.Tests.Sequences.TestDoubles.Repositories;

namespace RecklessSpeech.Application.Write.Sequences.Tests.Sequences.Enrich;

public class CaseOfMissingSequence
{
    private readonly EnrichDutchSequenceCommandHandler sut;
    private readonly InMemoryTestSequenceRepository sequenceRepository;

    public CaseOfMissingSequence()
    {
        sequenceRepository = new();
        
        this.sut = new(
            sequenceRepository,
            new DummyExplanationRepository(),
            new DummyDictionaryGateway());
    }

    [Fact]
    public async Task Should_do_nothing_if_sequence_not_found()
    {
        //Arrange
        SequenceBuilder sequenceBuilder = SequenceBuilder.Create(Guid.Parse("2D40A46C-A850-4A44-8DC5-1300B1318A8C"));
        this.sequenceRepository.Feed(sequenceBuilder.BuildDomain());

        IReadOnlyCollection<IDomainEvent> events = await this.sut.Handle(SequenceBuilder.Create(Guid.Parse("C5686F57-60F4-483D-B21F-C2CB726EDC23"))
            .BuildEnrichCommand(), CancellationToken.None);

        events.Should().BeEmpty();
    }

}