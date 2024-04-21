using System.Threading.Tasks;

namespace Core.Network.HttpClient
{
    public interface IHttpClient
    {
        Task<HttpResponse<T>> GetAsync<T>(string url);
    }
}