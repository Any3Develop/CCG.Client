using CardGame.Services.CommandService;

namespace CardGame.Cards
{
    public struct PlaceCardProtocol : IProtocol
    {
        public string SessionId;
        public string CardId;
        public FieldType ToPlace;
        public int Place;
        /// <summary>
        /// Can be empty
        /// </summary>
        public FieldType BeforePlace;
    }
}