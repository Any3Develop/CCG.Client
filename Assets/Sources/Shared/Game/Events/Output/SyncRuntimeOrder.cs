using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Game.Events.Output
{
    public class SyncRuntimeOrder : GameEvent
    {
        public IRuntimeOrderData RuntimeData { get; set; }
    }
}