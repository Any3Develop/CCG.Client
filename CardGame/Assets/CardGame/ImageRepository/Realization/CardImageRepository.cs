using System.Linq;
using System.Threading.Tasks;
using CardGame.Utils;
using UnityEngine;
using UnityEngine.Networking;

namespace CardGame.ImageRepository
{
    public class ImageRepository : IImageRepository
    {
        private readonly ConnectionConfig _config;
        public ImageRepository()
        {
            var textAsset = Resources.Load<TextAsset>("ConnectionConfig");
            _config = JsonUtility.FromJson<ConnectionConfig>(textAsset.text);;
        }
        public async Task<Sprite> Get()
        {
            var connected = Connection();
            await connected;
            if (!connected.Result)
            {
                Debug.LogWarning($"does not have connection to : {_config.Url}");
                return null;
            }
            
            var request = UnityWebRequestTexture.GetTexture(_config.Url.Replace(" ","%20"));
            var requestAsync = request.SendWebRequest();
            while (!requestAsync.isDone)
            {
                await Task.Yield();
            }
            if (request.isHttpError)
            {
                Debug.LogErrorFormat("Request error : {0}\n" + "Downloaded : {1}", 
                                     request.error, 
                                     request.downloadHandler.text);
                return null;
            }
            while (!request.isDone || request.downloadProgress < 1)
            {
                await Task.Yield();
            }

            var texture = DownloadHandlerTexture.GetContent(request);
            var sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one / 2f);
            return sprite;
        }

        public async Task<bool> Connection()
        {
            var request = UnityWebRequest.Get(_config.ServerUrl);
            var requestAsync = request.SendWebRequest();
            while (!requestAsync.isDone)
            {
                await Task.Yield();
            }

            return !request.isNetworkError && request.isDone;
        }
    }
}