using System.Collections.Generic;
using Demo.Core.Game.Enums;

namespace Demo.Core.Abstractions.Game.RuntimeData
{
    public interface IRuntimeObjectData : IRuntimeData
    {
        string OwnerId { get; }
        List<IRuntimeStatData> Stats { get; }
        RuntimeState PreviousState { get; set; }
        RuntimeState State { get; set; }
    }
}