using System;
using Demo.Core.Abstractions.Game.Data;
using Demo.Core.Game.Data.Enums;

namespace Demo.Core.Game.Data
{
    public abstract class ObjectData : IData
    {
        public string Id { get; set; }
        public ObjectType Type { get; set; }
        public string Title { get; set; }
        public string[] StatIds { get; set; } = Array.Empty<string>();
        public string[] EffectIds { get; set; } = Array.Empty<string>();
    }
}