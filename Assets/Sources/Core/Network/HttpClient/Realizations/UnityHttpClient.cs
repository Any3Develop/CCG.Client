using System;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

namespace Core.Network.HttpClient
{
    public class UnityHttpClient : IHttpClient
    {
        public async Task<HttpResponse<T>> GetAsync<T>(string url)
        {
            try
            {
                PrepareRequestUrl(ref url);
                var request = PrepareRequest<T>(url);
                try
                {
                    var requestAsync = request.SendWebRequest();
                    while (!requestAsync.isDone || request.downloadProgress < 1)
                        await Task.Yield();

                    if (request.result == UnityWebRequest.Result.Success)
                        return new HttpResponse<T>(DeserializeResponse<T>(request), request.responseCode, request.error, request.downloadHandler.text);

                    throw new Exception($"Request error: {request.error}\n" +
                                        $"Downloaded: {request.downloadHandler.text}");
                }
                catch (Exception e)
                {
                    Debug.LogError(e);
                    return new HttpResponse<T>(null, request.responseCode, request.error, request.downloadHandler.text);
                }
            }
            catch (Exception e)
            {
                Debug.LogErrorFormat("Request error: {0}", e);
                return new HttpResponse<T>(null, 400, e.Message, null);
            }
        }

        protected virtual void PrepareRequestUrl(ref string url)
        {
            url = url.Replace(" ", "%20");
        }

        protected virtual UnityWebRequest PrepareRequest<T>(string url)
        {
            return typeof(Texture).IsAssignableFrom(typeof(T)) 
                ? UnityWebRequestTexture.GetTexture(url)
                : UnityWebRequest.Get(url);
        }

        protected virtual object DeserializeResponse<T>(UnityWebRequest request)
        {
            var requestType = typeof(T);
            if (requestType == typeof(string))
                return request.downloadHandler.text;
            
            return typeof(Texture).IsAssignableFrom(requestType) 
                ? DownloadHandlerTexture.GetContent(request) 
                : JsonUtility.FromJson(request.downloadHandler.text, requestType);
        }
    }
}