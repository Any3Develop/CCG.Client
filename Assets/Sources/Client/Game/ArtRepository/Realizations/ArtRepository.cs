using System.IO;
using System.Net;
using System.Threading.Tasks;
using CardGame.Services.StorageService;
using Core.Network;
using Core.Network.HttpClient;
using Core.PictureStorage;
using UnityEngine;

namespace Core.ArtRepository
{
    public class ArtRepository : IArtRepository
    {
        private readonly IHttpClient httpClient;
        private readonly IStorage<PictureModel> pictureStorage;
        private readonly string url;

        public ArtRepository(
            IHttpClient httpClient,
            IStorage<PictureModel> pictureStorage)
        {
            this.httpClient = httpClient;
            this.pictureStorage = pictureStorage;
            url = Path.Combine(Urls.ArtApiUrl, "200/300");
        }

        public async Task<Texture> GetAsync(string id)
        {
            if (pictureStorage.HasEntity(id))
                return pictureStorage.Get(id)?.Texture;

            var response = await httpClient.GetAsync<Texture>(url);
            if (response.Status == HttpStatusCode.NotFound)
                throw new ArtNotFoundException(response.Message, id);

            if (response.Status != HttpStatusCode.OK)
                throw new RemoteArtException(response.Message, id);

            pictureStorage.Add(new PictureModel {Id = id, Texture = response});
            return response;
        }
    }
}