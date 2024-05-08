using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Game.Events.Output
{
    public class AddedObjectStat : GameEvent
    {
        public IRuntimeStatData RuntimeData { get; set; }
    }
}