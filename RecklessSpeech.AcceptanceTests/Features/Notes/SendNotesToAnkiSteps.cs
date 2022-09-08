using FluentAssertions;
using RecklessSpeech.Application.Write.Sequences.Tests.Notes;
using RecklessSpeech.Infrastructure.Sequences;
using RecklessSpeech.Shared.Tests.Notes;
using RecklessSpeech.Shared.Tests.Sequences;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Features.Notes;

[Binding, Scope(Feature = "Send Notes to Anki")]
public class SendNotesToAnkiSteps : StepsBase
{
    private readonly Guid sequenceId;
    private readonly InMemorySequencesDbContext dbContext;
    private readonly SpyNoteGateway spyNoteGateway;
    private const string contentForQuestion = "<style>some html here to be found in sequence and note";

    public SendNotesToAnkiSteps(ScenarioContext context) : base(context)
    {
        this.sequenceId = Guid.Parse("977343D4-0432-4BDF-BE78-5731C45CE00A");
        this.dbContext = base.GetService<InMemorySequencesDbContext>();
        this.spyNoteGateway = base.GetService<SpyNoteGateway>();
    }

    [Given(@"an existing sequence")]
    public void GivenAnExistingSequence()
    {
        SequenceBuilder? builder = SequenceBuilder.Create(sequenceId) with
        {
            HtmlContent = new(contentForQuestion)
        };
        dbContext.Sequences.Add(builder.BuildEntity());
    }

    [When(@"the user sends the sequence to Anki")]
    public async Task WhenTheUserSendsTheSequenceToAnki()
    {
        await this.Client.Latest().SequenceRequests().SendToAnki(new List<Guid>() {sequenceId});
    }

    [Then(@"a corresponding note is sent to Anki")]
    public void ThenACorrespondingNoteIsSentToAnki()
    {
        NoteBuilder? builder = NoteBuilder.Create(sequenceId) with
        {
            Question = new(contentForQuestion)
        };
        this.spyNoteGateway.Notes.Should().ContainEquivalentOf(builder.BuildDto());
    }
}