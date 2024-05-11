namespace Shared.Game.Events.Output
{
    public class CardPositionChanged : GameEvent
    {
        public int Id { get; set; }
        public int? Position { get; set; }
    }
}