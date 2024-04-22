using System.Collections.Generic;
using Demo.Core.Abstractions.Game.RuntimeData;
using Demo.Core.Game.Enums;

namespace Demo.Core.Game.RuntimeData
{
    public abstract class RuntimeObjectData : IRuntimeObjectData
    {
        public int Id { get; set; } = -1;
        public string DataId { get; set; }
        public string OwnerId { get; set; }
        public List<IRuntimeStatData> Stats { get; set; } = new();
        public RuntimeState State { get; set; } = 0;
        public RuntimeState PreviousState { get; set; } = 0;
    }
}