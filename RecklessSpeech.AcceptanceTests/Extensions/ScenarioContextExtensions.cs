using RecklessSpeech.AcceptanceTests.Configuration;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Extensions
{
    public static class ScenarioContextExtensions
    {
        private const string HttpError = "HttpError";

        public static void SetError(this ScenarioContext context, HttpTestServerException exception)
        {
            context.Set(exception, HttpError);
        }

        public static bool TryGetError(this ScenarioContext context, out HttpTestServerException error)
        {
            return context.TryGetValue(HttpError, out error);
        }

        public static void RemoveError(this ScenarioContext context)
        {
            context.Remove(HttpError);
        }
    }
}
