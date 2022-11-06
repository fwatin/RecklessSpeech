namespace RecklessSpeech.AcceptanceTests.Configuration.Clients;

public interface ITestsClient
{
    HttpClient Client { get; }
    Task Initialize();
    Task<T> Post<T>(string path, object? parameters = null);
    Task<T> Get<T>(string path, object? parameters = null);
    Task<T> Put<T>(string path, object? parameters = null);
}