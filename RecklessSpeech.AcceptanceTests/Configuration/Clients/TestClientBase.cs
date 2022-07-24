using System.Text;
using Flurl;
using Newtonsoft.Json;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Configuration.Clients;

public class TestClientBase : IDisposable
{
    private readonly ScenarioContext context;
    public HttpClient Client { get; }

    protected TestClientBase(ScenarioContext context, HttpClient client)
    {
        this.context = context;
        Client = client;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            this.Client.Dispose();
        }
    }

    public void Dispose()
    {
        this.Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<T> Post<T>(string path, object? parameters = null)
    {
        using HttpResponseMessage? response = await this.ExecuteRequest(HttpMethod.Post, path, parameters);
        if (response!.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            T? content = JsonConvert.DeserializeObject<T>(json);
            return content!;
        }

        return default!;
    }

    private async Task<HttpResponseMessage?> ExecuteRequest(HttpMethod method, string path, object? parameters = null)
    {
        try
        {
            using HttpRequestMessage request = BuildMessage(method, path, parameters);
            HttpResponseMessage response = await this.Client.SendAsync(request);
            return response;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e);
        }

        return new HttpResponseMessage();
    }

    private static HttpRequestMessage BuildMessage(HttpMethod method, string path, object? parameters)
    {
        if (parameters != null && parameters.GetType() == typeof(MultipartFormDataContent))
            return BuildMultiPartMessage(method, path, (MultipartFormDataContent) parameters);
        return BuildJsonMessage(method, path, parameters);
    }

    private static HttpRequestMessage BuildJsonMessage(HttpMethod method, string path, object? parameters)
    {
        if (method == HttpMethod.Post || method == HttpMethod.Put)
        {
            HttpRequestMessage request = new(method, new Uri(path, UriKind.RelativeOrAbsolute));
            if (parameters != null)
            {
                request.Content = new StringContent(JsonConvert.SerializeObject(parameters), Encoding.UTF8,
                    "application/json");
            }

            return request;
        }

        if (parameters != null)
        {
            path = path.SetQueryParams(parameters);
        }

        return new HttpRequestMessage(method, new Uri(path, UriKind.RelativeOrAbsolute));
    }

    private static HttpRequestMessage BuildMultiPartMessage(HttpMethod method, string path, MultipartFormDataContent multipartContent)
    {
        HttpRequestMessage request = new(method, new Uri(path, UriKind.RelativeOrAbsolute))
        {
            Content = multipartContent
        };
        return request;
    }
}