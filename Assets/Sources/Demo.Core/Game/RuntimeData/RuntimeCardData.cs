using Demo.Core.Abstractions.Game.RuntimeData;

namespace Demo.Core.Game.RuntimeData
{
    public class RuntimeCardData : RuntimeObjectData, IRuntimeCardData
    {
        public int Position { get; set; } = -1;
    }
}