using System.Collections.Generic;
using Demo.Core.Abstractions.Game.Runtime.Data;
using Demo.Core.Game.Data.Enums;

namespace Demo.Core.Game.Runtime.Data
{
    public abstract class RuntimeObjectData : IRuntimeObjectData
    {
        public int Id { get; set; } = -1;
        public string DataId { get; set; }
        public string OwnerId { get; set; }
        public List<string> EffectIds { get; set; } = new();
        public List<IRuntimeEffectData> Applied { get; set; } = new();
        public List<IRuntimeStatData> Stats { get; set; } = new();
        public ObjectState State { get; set; } = 0;
        public ObjectState PreviousState { get; set; } = 0;
    }
}