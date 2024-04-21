using System;
using CardGame.Services.StorageService;

namespace CardGame.Cards
{
    [Serializable]
    public struct CardModel : IStorageItem
    {
        string IStorageItem.Id => ModelId;
        public string ModelId;
        public string TypeName;
    }
}