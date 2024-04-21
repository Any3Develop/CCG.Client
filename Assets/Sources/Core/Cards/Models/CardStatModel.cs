using System;
using CardGame.Services.StatsService;
using CardGame.Services.StorageService;

namespace CardGame.Cards
{
    [Serializable]
    public struct CardStatModel : IStorageEntity
    {
        string IStorageEntity.Id => ModelId;
        public string ModelId;
        public StatModel[] Stats;
    }
}