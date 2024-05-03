using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Game.Runtime.Data
{
    public class RuntimeCardData : RuntimeObjectData, IRuntimeCardData
    {
        public int? Position { get; set; }
    }
}