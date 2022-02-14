using CardGame.Services.CommandService;

namespace CardGame.Cards
{
    public struct CreateCardSceneObjectProtocol : IProtocol
    {
        public string Id;
        public string SessionId;
    }
}