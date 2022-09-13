using System.Net;
using FluentAssertions;
using RecklessSpeech.AcceptanceTests.Configuration;
using RecklessSpeech.AcceptanceTests.Extensions;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Features
{
    [Binding]
    internal sealed class CommonSteps : StepsBase
    {
        public CommonSteps(ScenarioContext scenarioContext) : base(scenarioContext)
        {
        }

        [Then(@"an error is thrown with an HTTP status (\d*) and error type ""(.*)""")]
        public void ThenAnErrorIsThrownWithAnHttpStatusAndErrorType(int httpErrorCode, string errorType)
        {
            this.Context.TryGetError(out HttpTestServerException? error);
            error.Should().NotBeNull();
            error.StatusCode.Should().Be((HttpStatusCode)httpErrorCode);
            error.Details.Type.Should().Be(errorType);
            this.Context.RemoveError();
        }
    }
}
