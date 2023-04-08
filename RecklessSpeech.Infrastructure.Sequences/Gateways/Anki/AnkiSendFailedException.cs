namespace RecklessSpeech.Infrastructure.Sequences.Gateways.Anki
{
    public class AnkiSendFailedException : Exception
    {
        public AnkiSendFailedException(string message):base(message)
        {
            
        }
    }
}