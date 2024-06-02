using System.Collections.Generic;
using Client.Common.Abstractions.Network;
using Client.Common.Network.Exceptions;
using Client.Game.Abstractions.Context.EventProcessors;
using Newtonsoft.Json;
using Shared.Abstractions.Game.Events;
using Shared.Common.Logger;
using Shared.Common.Network;
using Shared.Common.Network.Data;
using Shared.Common.Network.Exceptions;
using Shared.Game.Utils;

namespace Client.Game.Network
{
    public class GameMessegeHandler : IMessegeHandler
    {
        private readonly IGameEventQueueRemoteProcessor queueRemoteProcessor;
        public GameMessegeHandler(IGameEventQueueRemoteProcessor queueRemoteProcessor)
        {
            this.queueRemoteProcessor = queueRemoteProcessor;
        }

        public void Handle(Message message)
        {
            switch (message.Route)
            {
                case Route.GameEvent:
                    queueRemoteProcessor.Process(Deserialize<List<IGameEvent>>(message));
                    break;

                case Route.Auth:
                    throw new NotAuthrorizedException(message.Data);
                
                case Route.Command:
                default: SharedLogger.Warning($"Server sent route: '{message.Route}' which client can't handle. Message:\n{JsonConvert.SerializeObject(message)}");
                    break;
            }
        }

        private T Deserialize<T>(Message message)
        {
            return JsonConvert.DeserializeObject<T>(message.Data, SerializeExtensions.GetDeserializeSettingsByType<T>());
        }
    }
}