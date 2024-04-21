using CardGame.Services.StorageService;
using UnityEngine;

namespace CardGame.ImageRepository
{
    public class PictureModel : IStorageEntity
    {
        public string Id { get; set; }
        public Texture Texture { get; set; }
    }
}