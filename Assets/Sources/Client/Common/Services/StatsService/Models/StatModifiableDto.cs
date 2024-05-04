using System;
using System.Collections.Generic;

namespace CardGame.Services.StatsService
{
    [Serializable]
    public class StatModifiableDto : StatDto
    {
        public List<StatModifierData> Modifiers;

        public StatModifiableDto(StatModel initModel) : base(initModel)
        {
            Modifiers = new List<StatModifierData>();
        }
    }
}