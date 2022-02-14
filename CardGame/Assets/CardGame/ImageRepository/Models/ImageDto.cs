using CardGame.Services.StorageService;
using UnityEngine;

namespace CardGame.ImageRepository
{
    public class ImageDto : IStorageItem
    {
        string IStorageItem.Id => Id;
        public string Id;
        public Sprite Image;
    }
}