using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Game.Events.Output
{
    public class SyncRuntimeId : GameEvent
    {
        public IRuntimeIdData RuntimeData { get; set; }
    }
}