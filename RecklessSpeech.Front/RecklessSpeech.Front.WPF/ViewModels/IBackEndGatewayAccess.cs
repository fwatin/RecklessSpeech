using System.Net.Http;
using System.Threading.Tasks;

namespace RecklessSpeech.Front.WPF.ViewModels
{
    public interface IBackEndGatewayAccess
    {
        Task PostAsync(string url, MultipartFormDataContent content);
        Task<HttpResponseMessage> GetAsync(string url);
        Task SendAsync(HttpRequestMessage request);
    }
}
