using System.Text;
using Flurl;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RecklessSpeech.AcceptanceTests.Extensions;
using TechTalk.SpecFlow;

namespace RecklessSpeech.AcceptanceTests.Configuration.Clients;

public class TestClientBase : IDisposable
{
    private readonly ScenarioContext context;
    public HttpClient Client { get; }

    protected TestClientBase(ScenarioContext context, HttpClient client)
    {
        this.context = context;
        this.Client = client;
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
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    public async Task<T> Post<T>(string path, object? parameters = null)
    {
        using HttpResponseMessage? response = await ExecuteRequest(HttpMethod.Post, path, parameters);
        if (response!.IsSuccessStatusCode)
        {
            string json = await response.Content.ReadAsStringAsync();
            T? content = JsonConvert.DeserializeObject<T>(json);
            return content!;
        }

        return default!;
    }
    
    public async Task<T> Get<T>(string path, object? parameters = null)
    {
        using HttpResponseMessage? response = await ExecuteRequest(HttpMethod.Get, path, parameters);
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
            await RecordErrorIfAny(response);
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
    
    private async Task RecordErrorIfAny(HttpResponseMessage response)
    {
        try
        {
            response.EnsureSuccessStatusCode();
        }
        catch (HttpRequestException)
        {
            await HandleDefaultHttpException();
        }

        async Task HandleDefaultHttpException()
        {
            string content = await response.Content.ReadAsStringAsync();
            ProblemDetails details = JsonConvert.DeserializeObject<ProblemDetails>(content)!;
            this.context.SetError(new HttpTestServerException(response.StatusCode, details));
        }
    }
}