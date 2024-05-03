using System;
using CardGame.Services.StorageService;

namespace CardGame.Cards
{
    [Serializable]
    public struct CardModel : IStorageEntity
    {
        string IStorageEntity.Id => ModelId;
        public string ModelId;
        public string TypeName;
    }
}