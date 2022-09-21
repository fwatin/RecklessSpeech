using RecklessSpeech.Front.WPF.App.ViewModels;

namespace RecklessSpeech.Front.Tests.SequencePage.ViewModel;

public class SpyBackEndGatewayAccess : IBackEndGatewayAccess
{
    public Task PostAsync(string url, MultipartFormDataContent content)
    {
        return Task.CompletedTask;
    }
    public async Task<HttpResponseMessage> GetAsync(string url)
    {
        string content = "[{\"id\":\"83d07ba4-402b-424d-8b41-5b81c4610a2e\",\"word\":\"gimmicks\",\"explanation\":null}]";
        HttpResponseMessage message = new()
        {
            Content = new StringContent(content)
        };
        return await Task.FromResult(message);
    }
    public Task SendAsync(HttpRequestMessage request)
    {
        throw new NotImplementedException();
    }
}