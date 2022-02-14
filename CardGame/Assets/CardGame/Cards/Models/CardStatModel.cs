using System;
using CardGame.Services.StatsService;
using CardGame.Services.StorageService;

namespace CardGame.Cards
{
    [Serializable]
    public struct CardStatModel : IStorageItem
    {
        string IStorageItem.Id => ModelId;
        public string ModelId;
        public StatModel[] Stats;
    }
}