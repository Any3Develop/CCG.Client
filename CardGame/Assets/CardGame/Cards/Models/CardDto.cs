using System;
using CardGame.Services.StorageService;

namespace CardGame.Cards
{
    [Serializable]
    public enum FieldType
    {
        OffScreen = 0,
        Hands,
        Table
    }
    [Serializable]
    public class CardDto : IStorageItem
    {
        string IStorageItem.Id => Id;
        public string Id;
        public string ModelId;
        public FieldType CurrentField;
        public int Place;
    }
}