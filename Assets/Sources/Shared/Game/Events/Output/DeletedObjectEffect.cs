using Shared.Abstractions.Game.Runtime.Data;

namespace Shared.Game.Events.Output
{
    public class DeletedObjectEffect : GameEvent
    {
        public IRuntimeEffectData RuntimeData { get; set; }
    }
    public class DeletedObjectStat : GameEvent
    {
        public IRuntimeStatData RuntimeData { get; set; }
    }
    public class AddedObjectStat : GameEvent
    {
        public IRuntimeStatData RuntimeData { get; set; }
    }
}