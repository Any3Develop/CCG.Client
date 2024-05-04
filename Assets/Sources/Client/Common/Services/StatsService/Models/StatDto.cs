using System;
using CardGame.Services.StorageService;

namespace CardGame.Services.StatsService
{
    [Serializable]
    public class StatDto : IStorageEntity
    {
        string IStorageEntity.Id => StatId;
        public string StatId;
        public string StatType;

        public StatDto(StatModel initModel)
        {
            StatId = initModel.StatID;
            StatType = initModel.StatType;
        }
    }
}