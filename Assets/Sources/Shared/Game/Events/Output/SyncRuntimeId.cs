namespace Shared.Game.Events.Output
{
    public class SyncRuntimeId : GameEvent
    {
        public int Current { get; set; }
    }
}