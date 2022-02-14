using System;
using CardGame.Services.CommandService;

namespace CardGame.Services.InputService
{
    public readonly struct InputControllerBatchActionProtocol : IProtocol
    {
        public readonly Type[] Composite;
        public readonly bool Value; 
        public InputControllerBatchActionProtocol(bool value, params Type[] layers)
        {
            Value = value;
            Composite = layers ?? new Type[0];
        }
    }
}