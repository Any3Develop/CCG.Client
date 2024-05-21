using System;
using System.Linq;
using Newtonsoft.Json;
using Server.Domain.Contracts.Messanger;
using Server.Domain.Contracts.Sessions;
using Shared.Abstractions.Game.Commands;
using Shared.Common.Logger;
using Shared.Common.Network;
using Shared.Game.Utils;

namespace Server.Application.Messenger
{
    public class MessengerHandler : IMessengerHandler, IDisposable
    {
        private readonly IRuntimeSessionRepository sessionRepository;
        public event Action<string, Message> CallBack;

        public MessengerHandler(IRuntimeSessionRepository sessionRepository)
        {
            this.sessionRepository = sessionRepository;
        }

        public void Handle(IClient client, Message message)
        {
            switch (message.Route)
            {
                case Route.Command:
                {
                    if (!client.IsAuthorized)
                        throw new NotAuthrorizedException();
                    
                    var session = sessionRepository.GetByUserId(client.UserId);
                    if (session == null)
                        throw new Exception($"There's no any available session for client : {client.UserId}.");
                    
                    var model = JsonConvert.DeserializeObject<ICommandModel>(message.Data, SerializeExtensions.GetDeserializeSettingsByType<ICommandModel>());
                    session.Context.CommandProcessor.Execute(client.UserId, model);
                    break;
                }
                
                case Route.Auth:
                {
                    var session = sessionRepository.GetFreeSession();
                    if (session == null)
                        throw new Exception($"There's no any free session.");

                    var player = session.Context.PlayersCollection.First(x => !x.RuntimeData.Ready);
                    client.SetUserId(player.RuntimeData.DataId);
                    CallBack?.Invoke(player.RuntimeData.OwnerId, new Message
                    {
                        Route = Route.Auth,
                        Data = player.RuntimeData.OwnerId
                    });
                    break;
                }
                
                case Route.DropConnection:
                case Route.GameEvent:
                default: SharedLogger.Warning($"Server cant handle the route : {message.Route}");
                    break;
            }
        }

        public void Dispose()
        {
            CallBack = null;
        }
    }
}