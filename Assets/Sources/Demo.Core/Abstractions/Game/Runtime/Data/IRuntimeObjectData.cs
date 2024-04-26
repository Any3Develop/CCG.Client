using System.Collections.Generic;
using Demo.Core.Game.Data.Enums;

namespace Demo.Core.Abstractions.Game.Runtime.Data
{
    public interface IRuntimeObjectData : IRuntimeDataBase
    {
        List<string> EffectIds { get; }
        List<IRuntimeEffectData> Applied { get; }
        List<IRuntimeStatData> Stats { get; }
        ObjectState PreviousState { get; set; }
        ObjectState State { get; set; }
    }
}