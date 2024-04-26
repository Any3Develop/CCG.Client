using Demo.Core.Abstractions.Game.RuntimeData;

namespace Demo.Core.Game.Runtime.Data
{
    public class RuntimeCardData : RuntimeObjectData, IRuntimeCardData
    {
        public int? Position { get; set; }
    }
}