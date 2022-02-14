using System;
using CardGame.Services.CommandService;

namespace CardGame.Cards
{
    public struct AddCardProtocol : IProtocol
    {
        public string CardId;
        public string ModelId;
        public string SessionId;
        public object[] Params;
        public AddCardProtocol(string sessionId,
                               string modelID, 
                               params object[] args)
        {
            SessionId = sessionId;
            CardId = Guid.NewGuid().ToString();
            ModelId = modelID;
            Params = args;
        }
    }
}