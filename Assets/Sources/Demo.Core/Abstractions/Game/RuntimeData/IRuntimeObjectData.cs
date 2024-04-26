using System.Collections.Generic;
using Demo.Core.Game.Enums;

namespace Demo.Core.Abstractions.Game.RuntimeData
{
    public interface IRuntimeObjectData : IRuntimeDataBase
    {
        List<string> EffectIds { get; }
        List<IRuntimeEffectData> Applied { get; }
        List<IRuntimeStatData> Stats { get; }
        RuntimeState PreviousState { get; set; }
        RuntimeState State { get; set; }
    }
}