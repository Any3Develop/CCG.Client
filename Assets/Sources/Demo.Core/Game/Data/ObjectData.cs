using System;
using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Game.Enums;

namespace Demo.Core.Game.Data
{
    public abstract class ObjectData : IData
    {
        public string Id { get; set; }
        public ObjectType Type { get; set; }
        public string Title { get; set; }
        public StatData[] Stats { get; set; } = Array.Empty<StatData>();
        public string[] EffectIds { get; } = Array.Empty<string>();
    }
}