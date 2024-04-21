using System;

namespace CardGame.Services.StatsService
{
    [Serializable]
    public struct StatModel
    {
       public string StatID;
       public string StatType;
       public float BaseValue;
       public float InitValue;
    }
}