namespace Shared.Game.Data
{
    public class SessionPlayer
    {
        public string Id { get; set; }
        public string DeckId { get; set; }
        public string HeroId { get; set; } // TODO
        public string[] DeckCards { get; set; }
    }
}