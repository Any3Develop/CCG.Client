using Demo.Core.Abstractions.Game.Runtime.Data;

namespace Demo.Core.Game.Runtime.Data
{
    public class RuntimeCardData : RuntimeObjectData, IRuntimeCardData
    {
        public int? Position { get; set; }
    }
}