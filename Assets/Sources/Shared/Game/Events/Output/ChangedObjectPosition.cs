namespace Shared.Game.Events.Output
{
    public class ChangedObjectPosition : GameEvent
    {
        public int Id { get; set; }
        public int? Position { get; set; }
    }
}