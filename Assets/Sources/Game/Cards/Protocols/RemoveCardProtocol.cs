using CardGame.Services.CommandService;

namespace CardGame.Cards
{
    public struct RemoveCardProtocol : IProtocol
    {
        public string CardId;
        public string SessionId;
        public FieldType FieldType;
        public int Place;
    }
}