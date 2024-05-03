using System;
using System.Collections.Generic;

namespace CardGame.Services.StatsService
{
    [Serializable]
    public class StatAttributeDto : StatModifiableDto
    {
        public float ScaleValue;
        public List<StatLinkerData> Linkers;

        public StatAttributeDto(StatModel initModel) : base(initModel)
        {
            ScaleValue = 0;
            Linkers = new List<StatLinkerData>();
        }
    }
}