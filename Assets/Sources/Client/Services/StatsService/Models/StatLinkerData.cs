using System;

namespace CardGame.Services.StatsService
{
    [Serializable]
    public struct StatLinkerData
    {
        public string TypeName;
        public string LinkerId;
        public string StatId;
        public float Value;
    }
}