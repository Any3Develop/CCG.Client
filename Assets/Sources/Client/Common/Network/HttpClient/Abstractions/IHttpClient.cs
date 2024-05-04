using System.Threading.Tasks;

namespace Client.Common.Network.HttpClient
{
    public interface IHttpClient
    {
        Task<HttpResponse<T>> GetAsync<T>(string url);
    }
}