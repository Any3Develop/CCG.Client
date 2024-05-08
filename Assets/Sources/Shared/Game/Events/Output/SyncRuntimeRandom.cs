using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Game.Events.Output
{
    public class SyncRuntimeRandom : GameEvent
    {
        public IRuntimeRandomData RuntimeData { get; set; }
    }
}