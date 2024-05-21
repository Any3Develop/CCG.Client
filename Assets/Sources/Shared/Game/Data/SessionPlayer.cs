namespace Shared.Game.Data
{
    public class SessionPlayer
    {
        public string Id { get; set; }
        public string DeckId { get; set; }
        public string[] DeckCards { get; set; }
        public bool Ready { get; set; }
    }
}